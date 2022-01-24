using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.ServicesComposer;
using NLog;

namespace MobileBalanceHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IServicesComposer _servicesComposer;
        private static readonly Logger InfoLogger = LogManager.GetLogger("infoRules");
        private static readonly Logger ErrorLogger = LogManager.GetLogger("errorRules");
        private readonly IStringLocalizer<Localization.Localization> _localizer;

        public PaymentController(IServicesComposer servicesComposer, IStringLocalizer<Localization.Localization> localizer)
        {
            _servicesComposer = servicesComposer;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendFunds(PaymentData paymentData)
        {
            try
            {
                HttpContext.Response.Headers["RequestId"] = Guid.NewGuid().ToString();
                InfoLogger.Info($"Поступил запрос на пополнение баланса по номеру: {paymentData.PhoneNumber} " +
                                $"на сумму: {paymentData.Sum} c request id: {HttpContext.Response.Headers["RequestId"]}");
                var result = await _servicesComposer.ComposeServices(paymentData);
                if (result.IsSuccessStatusCode)
                {
                    InfoLogger.Info($"Платеж по номеру: {paymentData.PhoneNumber} на сумму: {paymentData.Sum} c request id: {HttpContext.Response.Headers["RequestId"]} принят в обработку");
                    var localizedString = _localizer.GetString("success");
                    var message = localizedString.Value.Replace("number", paymentData.PhoneNumber);
                    message = message.Replace("sum", paymentData.Sum.ToString(CultureInfo.InvariantCulture));
                    return Ok(message);
                }
                
                return BadRequest(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                ErrorLogger.Error(
                    $"При пополнении по номеру: {paymentData.PhoneNumber} на сумму: {paymentData.Sum} с c request id: {HttpContext.Response.Headers["RequestId"]} возникло исключение: {e.Message}");
                return BadRequest(_localizer.GetString("exceptionMessage").Value);
            }
        }
    }
}
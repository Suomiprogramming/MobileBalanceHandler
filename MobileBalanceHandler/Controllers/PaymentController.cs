using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public PaymentController(IServicesComposer servicesComposer)
        {
            _servicesComposer = servicesComposer;
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
                    return Ok($"Платеж по номеру {paymentData.PhoneNumber} на сумму {paymentData.Sum} тенге принят в обработку!");
                }
                
                return BadRequest(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                ErrorLogger.Error(
                    $"При пополнении по номеру: {paymentData.PhoneNumber} на сумму: {paymentData.Sum} с c request id: {HttpContext.Response.Headers["RequestId"]} возникло исключение: {e.Message}");
                return BadRequest("Возникла непредвиденная ошибка, приносим извинения!");
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.ServicesComposer;

namespace MobileBalanceHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IServicesComposer _servicesComposer;

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
                var result = await _servicesComposer.ComposeServices(paymentData);
                if (result.IsSuccessStatusCode)
                {
                    return Ok($"Платеж по номеру {paymentData.PhoneNumber} на сумму {paymentData.Sum} тенге принят в обработку!");
                }
                
                return BadRequest(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return BadRequest("Возникла непредвиденная ошибка, приносим извинения!");
            }
        }
    }
}
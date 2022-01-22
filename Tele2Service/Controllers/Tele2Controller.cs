using Microsoft.AspNetCore.Mvc;
using MobileBalanceHandler.Models;

namespace Tele2Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Tele2Controller : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddCash(PaymentData paymentData)
        {
            return Ok(paymentData);
        }
    }
}
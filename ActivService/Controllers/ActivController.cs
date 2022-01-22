using Microsoft.AspNetCore.Mvc;
using MobileBalanceHandler.Models;

namespace ActivService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult SendMoney(PaymentData paymentData)
        {
            return Ok(paymentData);
        }
    }
}
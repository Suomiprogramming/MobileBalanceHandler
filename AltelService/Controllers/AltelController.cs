using Microsoft.AspNetCore.Mvc;
using MobileBalanceHandler.Models;

namespace AltelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AltelController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddFunds(PaymentData paymentData)
        {
            return Ok(paymentData);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileBalanceHandler.Models;

namespace BeelineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeelineController : Controller
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddBalance(PaymentData paymentData)
        {
            return Ok(paymentData);
        }
    }
}
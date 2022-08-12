using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSapp.Hubs;
using POSapp.Models;

namespace POSapp
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {

        [HttpPost]
        public  IActionResult Post(string data)
        {
            var s = new PrintingExample(new PrintModel());
            return Ok(s);
        }
    }
}

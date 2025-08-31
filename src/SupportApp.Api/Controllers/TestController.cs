using Microsoft.AspNetCore.Mvc;
namespace SupportApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string Get()
        {
            return "test";
        }
    }
}

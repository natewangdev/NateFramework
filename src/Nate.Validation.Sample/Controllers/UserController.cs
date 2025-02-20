using Microsoft.AspNetCore.Mvc;

namespace Nate.Validation.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create(UserDto userDto)
        {
            // 验证通过后的业务逻辑
            return Ok();
        }
    }
}

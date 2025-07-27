using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // コントローラーのルートパス指定
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // api/user
        [HttpGet("profile")]
        // [Authorize]によるAPI保護
        [Authorize]

        // userIdとroleを取得
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst("userId")?.Value;
            var role = User.FindFirst("Roles")?.Value;

            return Ok(new
            {
                Message = "トークンによって保護されたデータです",
                UserId = userId,
                Role = role
            });
        }
    }
}

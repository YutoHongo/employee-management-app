using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.DTOs;

namespace Backend.Controllers
{
    // コントローラーをのルートパス指定
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        // 読み取り専用でAuthServiceを呼び出す
        private readonly AuthService _authService;

        // DIでAuthServiceを受け取る
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // api/auth/login
        [HttpPost("login")]

        // 引数requestが_authService内のリスト要素と一致するかの認証を行う為ValidateUserメソッドを実行
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _authService.ValidateUser(request.Email, request.Password);

            // 一致要素無しの場合401Unauthorizedエラー+メッセージ
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // 一致要素有の場合200 + トークン作成
            var token = JwtTokenGenerator.GenerateToken(user);
            return Ok(new LoginResponse
            {
                Token = token,
                Role = user.Role
            });
        }
    }
}
   
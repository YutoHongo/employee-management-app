using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RegistrationController : ControllerBase
    {
        // ダミーデータ
        private static readonly List<SignupRequest> _employees = new List<SignupRequest>();

        // 処理結果をログで出力
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        // 引数id(request.Id)に該当する_employee要素を出力する
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            // 該当idが存在しない場合NotFound()を返す
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            // 入力データが不備・不正の場合弾く
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("登録失敗：無効なリクエスト");
                return BadRequest(ModelState);
            }

            // Idを自動採番（初期値:1）
            int newId = _employees.Any() ? _employees.Max(e => e.Id)+1 : 1;
            request.Id = newId;

            // Request情報を_employeesリストに追加
            _employees.Add(request);
            
            _logger.LogInformation($"新規登録：{request.FullName}");
            // Request処理が正常に処理された場合に GetEmployeeByIdメソッドへのURLをロケーションヘッダとして案内、登録情報出力
            return CreatedAtAction(nameof(GetEmployeeById), new { id = request.Id }, request);
        }
    }
}
using Backend.Enums;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralController : ControllerBase
    {
        // DB作成までダミーデータ使用
        private static List<Employee> _employees = new()
        {
            new Employee
            {
                Id = 1,
                FullName = "TestEmpoyee1",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = Gender.男性,
                Address = "dummy1-1-1",
                JoinDate = new DateTime(2020, 6, 1),
                VacationRemaining = 5,
                CurrentWorkplace = "workplace1",
            },

            new Employee
            {
                Id = 2,
                FullName = "TestEmpoyee2",
                DateOfBirth = new DateTime(2001, 2, 2),
                Gender = Gender.女性,
                Address = "dummy2-2-2",
                JoinDate = new DateTime(2021, 7, 1),
                VacationRemaining = 10,
                CurrentWorkplace = "workplace2",
            },

            new Employee
            {
                Id = 3,
                FullName = "TestEmpoyee3",
                DateOfBirth = new DateTime(2003, 3, 3),
                Gender = Gender.その他,
                Address = "dummy3-3-3",
                JoinDate = new DateTime(2020, 6, 1),
                VacationRemaining = 15,
                CurrentWorkplace = "workplace3",
            }
        };

        // api/employees
        [HttpGet]

        // 従業員権限でのみ承認
        [Authorize(Roles = "General")]
        public ActionResult<Employee> GetMyEmployeeInfo()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            // userIdClaimがnullの場合またはint型に変換出来ない場合にUnauthorizedを返す
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("ユーザーIDが無効です");

            var employee = _employees.FirstOrDefault(e => e.Id == userId);

            // _employeesリスト内のIdにe.Idと同一要素が無い場合NotFoundを返す
            if (employee == null)
                return NotFound("該当する従業員情報が見つかりません");

            // _employeesリスト内のIdにe.Idと同一要素のみを(現在ログインしているユーザー情報)出力
            return Ok(employee);
        }
    }
}
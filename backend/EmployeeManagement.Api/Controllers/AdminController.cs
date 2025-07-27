using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Backend.Enums;
using Backend.DTOs;

[ApiController]
[Route("api/[controller]")]

public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;

    // ダミーデータ
    private static readonly List<Employee> _employees = new()
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
            CurrentWorkplace = "workplace1"
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
            CurrentWorkplace = "workplace2"
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
            CurrentWorkplace = "workplace3"
        }
    };

    private static List<EditEmployeeRequest> _editEmployee = new()
    {
        new EditEmployeeRequest
        {
            Id = 1,
            FullName = "TestEmpoyee1",
            DateOfBirth = new DateTime(2000, 1, 1),
            Gender = Gender.男性,
            Address = "dummy1-1-1",
            JoinDate = new DateTime(2020, 6, 1),
            VacationRemaining = 5,
            CurrentWorkplace = "workplace1"
        }
    };

    // ダミーデータ_employees全容出力
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        return Ok(_employees);
    }

    // 指定したIdの項目変更
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(int id, [FromBody] EditEmployeeRequest updateData)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("モデルエラー：{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var user = _editEmployee.FirstOrDefault(e => e.Id == id);
        if (user == null)
        {
            return NotFound("対象のユーザーが存在しません");
        }

        if (!string.IsNullOrEmpty(updateData.FullName))
            user.FullName = updateData.FullName;
        if (!string.IsNullOrEmpty(updateData.Address))
            user.Address = updateData.Address;
        if (!string.IsNullOrEmpty(updateData.CurrentWorkplace))
            user.CurrentWorkplace = updateData.CurrentWorkplace;

        return Ok(new {message = "ユーザー情報を更新しました", updateData = user});
    }
}
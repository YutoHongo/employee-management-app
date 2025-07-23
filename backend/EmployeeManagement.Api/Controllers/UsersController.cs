using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Backend.Enums;

[ApiController]
[Route("api/[controller]")]
// Admin権限でのみで承認
[Authorize(Roles = "Admin")]

public class UsersController : ControllerBase
{
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
            CurrentWorkplace = "workplace1",
            Status = Status.Active
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
            Status = Status.Active
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
            Status = Status.Retired
        }
    };

    // Employyee情報全容出力
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_employees);
    }
}
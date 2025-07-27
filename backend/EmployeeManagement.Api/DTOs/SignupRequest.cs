using Backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.DTOs
{
    public class SignupRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "氏名を入力してください")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "生年月日を入力してください")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "住所を入力してください")]
        public string Address { get; set; }

        [Required(ErrorMessage = "入社日を入力してください")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "勤務先を入力してください")]
        public string CurrentWorkplace { get; set; }

        [Required(ErrorMessage = "メールアドレスを入力してください")]
        [EmailAddress(ErrorMessage = "メールアドレスの入力形式が誤っています")]
        public string Email { get; set; }

        [Required(ErrorMessage = "パスワードを入力してください")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "パスワードは8文字以上で入力してください")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "パスワードは英大文字・小文字・数字を含む必要があります")]
        public string Password { get; set; }
    }
}
using Backend.Enums;
using System.Text.Json.Serialization;

namespace Backend.DTOs
{
    public class EditEmployeeRequest
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        public string Address { get; set; }

        public DateTime JoinDate { get; set; }

        public int VacationRemaining { get; set; }

        public string CurrentWorkplace { get; set; }
    }
}
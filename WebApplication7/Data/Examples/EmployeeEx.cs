using System.ComponentModel;

namespace WebApplication7.Data.Examples
{
    public class EmployeeEx
    {
        public int EmployeeId { get; set; }
        [DisplayName("Фамилия")]
        public string Surname { get; set; } 
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Прозвище")]
        public string Patronymic { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}

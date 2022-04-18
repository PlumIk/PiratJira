using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Email { get; set; }

        public virtual List<Project> Projects { get; set; } = new ();
        public virtual List<ProjectEmplyee> ProjectEmplyees { get; set; } = new();


    }
}

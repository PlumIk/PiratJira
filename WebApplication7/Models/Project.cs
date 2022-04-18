using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string CustomerCompanyName { get; set; }
        [Required]
        public string PerformerCompanyName { get; set; }
        [Required]
        public DateTime StartDate { get; set; } 
        [Required]
        public DateTime EndDatee { get; set; }
        [Required]
        public int Order { get; set; } = 15;
        

        public virtual List<Employee> Employees { get; set; } = new ();
        public virtual List<ProjectEmplyee> ProjectEmplyees { get; set; } = new();
    }
}

namespace WebApplication7.Models
{
    public class ProjectEmplyee
    {
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public bool IsManager { get; set; } =false;
    }
}

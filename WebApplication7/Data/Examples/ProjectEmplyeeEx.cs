using System.ComponentModel;

namespace WebApplication7.Data.Examples
{
    public class ProjectEmplyeeEx
    {
        [DisplayName("Пират")]
        public int EmployeeID { get; set; }
        [DisplayName("Квест")]
        public int ProjectID { get; set; }
        [DisplayName("Является первостепенно ответственным")]
        public string IsManager { get; set; } = "No";

        public string? EmployeeName { get; set; }
        public string? Project_oneName { get; set; }
    }
}

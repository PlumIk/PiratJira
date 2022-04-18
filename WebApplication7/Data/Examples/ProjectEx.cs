using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Data.Examples
{
    public class ProjectEx
    {
        public int ProjectId { get; set; }
        [DisplayName("Название квеста")]
        public string ProjectName { get; set; }
        [DisplayName("Заказчик")]
        public string CustomerCompanyName { get; set; }
        [DisplayName("Исполнитель")]
        public string PerformerCompanyName { get; set; }
        [DisplayName("Время начала")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DisplayName("Время конца")]
        public DateTime EndDate { get; set; }

        [DisplayName("Приоритет")]
        [Range(1, 15, ErrorMessage = "Приоритет должен быть от 1 до 15")]
        public int Order { get; set; } = 15;
    }
}

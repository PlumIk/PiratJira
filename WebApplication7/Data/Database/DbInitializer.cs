using WebApplication7.Data;

namespace WebApplication7.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any students.
            if (context.Employee.Any())
            {
                return;   // DB has been seeded
            }

            var emp_list = new Employee[]
            {
                new Employee{Name = "Лёша", Patronymic="France kid",Surname = "Silmeore", Email="Silmeore@za.bortom"},
                new Employee{Name = "Саша", Patronymic="Salt snail", Surname = "CapTawni", Email="NosferatI@na.parom"},
                new Employee{Name = "Саша", Patronymic="Gangplank diver",Surname = "SirApex", Email="Emris.rusalka.jdy"},
                new Employee{Name = "Билли", Patronymic="Бонс", Surname = "Умер",Email="apchhi@mail.cot"},
                new Employee{Name = "Олег", Patronymic="HappyOlega", Surname = "Галеонов",Email="est@mesto.yvas"},
                new Employee{Name = "Джек", Patronymic="Воробей", Surname = "Капитан",Email="Banochka@s.zemlicey"},
            };

            context.Employee.AddRange(emp_list);
            context.SaveChanges();
            
            var proj_list = new Project[]
            {
                new Project{ 
                    ProjectName ="Ограбить французов", CustomerCompanyName ="Билли бой", PerformerCompanyName="Нестабильный бриг",
                   Order =5, StartDate = new DateTime(2022, 4, 8), EndDatee=new DateTime(2022, 4, 22)},
                new Project{
                    ProjectName ="Добыть золота", CustomerCompanyName ="Жена", PerformerCompanyName="Шлюп дракона",
                   Order =2, StartDate = new DateTime(2022, 4, 17), EndDatee=new DateTime(2022, 4, 30)},
                new Project{
                    ProjectName ="Починить корабль", CustomerCompanyName ="Нестабльный бриг", PerformerCompanyName="Золотые пески",
                   Order =1, StartDate = new DateTime(2022, 4, 22), EndDatee=new DateTime(2022, 4, 23)},
            };

            context.Project.AddRange(proj_list);
            context.SaveChanges();

            var projemp_list = new ProjectEmplyee[]
            {
                new ProjectEmplyee{EmployeeID =1,ProjectID=1},
                new ProjectEmplyee{EmployeeID =2,ProjectID=1, IsManager=true},
                new ProjectEmplyee{EmployeeID =3,ProjectID=1},
                new ProjectEmplyee{EmployeeID =4,ProjectID=2},
                new ProjectEmplyee{EmployeeID =5,ProjectID=3, IsManager=true},
                new ProjectEmplyee{EmployeeID=6,ProjectID=2, IsManager=true}
            };

            context.ProjectEmplyee.AddRange(projemp_list);
            context.SaveChanges();
        }
    }
}

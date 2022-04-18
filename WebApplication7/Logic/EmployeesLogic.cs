using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Examples;
using WebApplication7.Models;

namespace WebApplication7.Logic
{
    public class EmployeesLogic
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<Employee> _data;
        public EmployeesLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        //Возвращаем хранящийся лист данных в нужном нам формате
        public List<EmployeeEx> GetData()
        {
            List<EmployeeEx> ret = new List<EmployeeEx>();
            foreach (var item in _data)
            {
                ret.Add(new EmployeeEx
                {
                    Surname = item.Surname,
                    Name = item.Name,
                    Email = item.Email,
                    Patronymic = item.Patronymic,
                    EmployeeId = item.EmployeeId,
                });
            }
            return ret;
        }
        //Получаем единственный экземпляр, используя id
        public EmployeeEx? GetOneById(int? id)
        {
            var dbOne = _context.Employee.Find(id);
            if (dbOne == null)
            {
                return null;
            }
            EmployeeEx ret = new EmployeeEx
            {
                Surname = dbOne.Surname,
                Name = dbOne.Name,
                Email = dbOne.Email,
                Patronymic = dbOne.Patronymic,
                EmployeeId = dbOne.EmployeeId,
            };    
            return ret;

        }

        //Получаемвесь список из таблице и храним его для обработки
        public void InitAllData()
        {
            _data =  _context.Employee.ToList();
        }

        //Преобразуем данные из используемого формата в формат для базы данных и добавляем новый объект
        public void Add(EmployeeEx addOne)
        {
            var toAdd = new Employee{
                Surname=addOne.Surname,
                Name=addOne.Name,
                Email=addOne.Email,
                Patronymic=addOne.Patronymic
            };
            _context.Add(toAdd);
            _context.SaveChanges();
        }

        //Преобразуем данные из используемого формата в формат для базы данных и обновляем объект
        public void Update(EmployeeEx updateOne)
        {
            var toAdd = new Employee
            {
                EmployeeId=updateOne.EmployeeId,
                Surname = updateOne.Surname,
                Name = updateOne.Name,
                Email = updateOne.Email,
                Patronymic = updateOne.Patronymic
            };
            _context.Update(toAdd);
            _context.SaveChanges();
        }

        //Удаляем объект по id
        public void DeleteConfirmed(int id)
        {
            var deleteOne = _context.Employee.Find(id);
            if (deleteOne != null)
            {
                _context.Employee.Remove(deleteOne);
                _context.SaveChanges();
            }
        }

        //Проверка на существование в базе данных
        public bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }

        //Вывести с подстрокой в любой из строк
        public void TakeWithWord(string searchNameString)
        {
            if (!String.IsNullOrEmpty(searchNameString))
            {
                _data = _data.Where(s => s.Name.Contains(searchNameString)
                                       || s.Surname.Contains(searchNameString)
                                       || s.Patronymic.Contains(searchNameString));
            }
        }

        //Задание сортировки. По умолчанию по имени
        public void SortBy(string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    _data = _data.OrderByDescending(s => s.Name);
                    break;
                case "Surname":
                    _data = _data.OrderBy(s => s.Surname);
                    break;
                case "surname_desc":
                    _data = _data.OrderByDescending(s => s.Surname);
                    break;
                case "Patronymic":
                    _data = _data.OrderBy(s => s.Patronymic);
                    break;
                case "patronymic_desc":
                    _data = _data.OrderByDescending(s => s.Patronymic);
                    break;
                case "Email":
                    _data = _data.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    _data = _data.OrderByDescending(s => s.Email);
                    break;
                default:
                    _data = _data.OrderBy(s => s.Name);
                    break;
            }
        }
    }
}

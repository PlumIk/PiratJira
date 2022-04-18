using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Examples;
using WebApplication7.Models;

namespace WebApplication7.Logic
{
    public class ProjectLogic
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<Project> _data;
        public ProjectLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        //Возвращаем хранящийся лист данных в нужном нам формате
        public List<ProjectEx> GetData()
        {
            List<ProjectEx> ret = new();
            foreach (var item in _data)
            {
                ret.Add(new ProjectEx
                {
                     CustomerCompanyName = item.CustomerCompanyName,
                     EndDate = item.EndDatee, 
                     Order = item.Order,
                     PerformerCompanyName = item.PerformerCompanyName,
                     ProjectId = item.ProjectId,  
                     ProjectName = item.ProjectName,    
                     StartDate = item.StartDate
                });
            }
            return ret;
        }

        //Получаем единственный экземпляр, используя id
        public ProjectEx? GetOneById(int? id)
        {
            var dbOne = _context.Project.Find(id);
            if (dbOne == null)
            {
                return null;
            }
            ProjectEx ret = new()
            {
                CustomerCompanyName = dbOne.CustomerCompanyName,
                EndDate = dbOne.EndDatee,
                Order = dbOne.Order,
                PerformerCompanyName = dbOne.PerformerCompanyName,
                ProjectId = dbOne.ProjectId,
                ProjectName = dbOne.ProjectName,
                StartDate = dbOne.StartDate
            };    
            return ret;

        }

        //Получаемвесь список из таблице и храним его для обработки
        public void InitAllData()
        {
            _data = _context.Project.ToList();
        }

        //Преобразуем данные из используемого формата в формат для базы данных и добавляем новый объект
        public void Add(ProjectEx addOne)
        {
            var toAdd = new Project
            {
                CustomerCompanyName = addOne.CustomerCompanyName,
                EndDatee = addOne.EndDate
                ,
                Order = addOne.Order,
                PerformerCompanyName = addOne.PerformerCompanyName,
                ProjectId = addOne.ProjectId,
                ProjectName = addOne.ProjectName,
                StartDate = addOne.StartDate

            };
            _context.Add(toAdd);
            _context.SaveChanges();
        }

        //Преобразуем данные из используемого формата в формат для базы данных и обновляем объект
        public void Update(ProjectEx updateOne)
        {
            var toAdd = new Project
            {
                CustomerCompanyName = updateOne.CustomerCompanyName,
                EndDatee = updateOne.EndDate,
                Order = updateOne.Order,
                PerformerCompanyName = updateOne.PerformerCompanyName,
                ProjectId = updateOne.ProjectId,
                ProjectName = updateOne.ProjectName,
                StartDate = updateOne.StartDate
            };
            _context.Update(toAdd);
            _context.SaveChanges();
        }

        //Удаляем объект по id
        public void Delete(int id)
        {
            var deleteOne = _context.Project.Find(id);
            if (deleteOne != null)
            {
                _context.Project.Remove(deleteOne);
                _context.SaveChanges();
            }
        }

        //Проверка на существование в базе данных
        public bool Project_oneExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }

        //Вывести с подстрокой в любой из строк
        public void TakeWithWord(string searchNameString)
        {
            if (!String.IsNullOrEmpty(searchNameString))
            {
                _data = _data.Where(s => s.PerformerCompanyName.Contains(searchNameString)
                                       || s.CustomerCompanyName.Contains(searchNameString)
                                       || s.ProjectName.Contains(searchNameString));
            }
        }

        //Взять все даты до
        public void TakeBeforeDate(DateTime date)
        {
            if (date != new DateTime()) {
                _data = _data.Where(s => s.StartDate <= date);
            }
        }

        //Взять все даты после
        public void TakeAfterDate(DateTime date)
        {
            if (date != new DateTime())
            {
                _data = _data.Where(s => s.EndDatee >= date);
            }
        }

        //Взять более приоритетные задачи
        public void TakeMore(int order)
        {
            if(order > 0 && order<=15)
            {
                _data = _data.Where(s => s.Order <= order);
            }
        }

        //Взять менее приоритетные задачи
        public void TakeLess(int order)
        {
            if (order > 0 && order <= 15)
            {
                _data = _data.Where(s => s.Order >= order);
            }
        }

        //Задание сортировки. По умолчанию по имени проекта
        public void SortBy(string sortOrder)
        {

            switch (sortOrder)
            {

                case "ProjectName_desc":
                    _data = _data.OrderByDescending(s => s.ProjectName);
                    break;
                case "CustomerCompanyName":
                    _data = _data.OrderBy(s => s.CustomerCompanyName);
                    break;
                case "CustomerCompanyName_desc":
                    _data = _data.OrderByDescending(s => s.CustomerCompanyName);
                    break;
                case "PerformerCompanyName":
                    _data = _data.OrderBy(s => s.PerformerCompanyName);
                    break;
                case "PerformerCompanyName_desc":
                    _data = _data.OrderByDescending(s => s.PerformerCompanyName);
                    break;
                case "StartDate":
                    _data = _data.OrderBy(s => s.StartDate);
                    break;
                case "StartDate_desc":
                    _data = _data.OrderByDescending(s => s.StartDate);
                    break;
                case "EndDate":
                    _data = _data.OrderBy(s => s.EndDatee);
                    break;
                case "EndDate_desc":
                    _data = _data.OrderByDescending(s => s.EndDatee);
                    break;
                case "Order":
                    _data = _data.OrderBy(s => s.Order);
                    break;
                case "Order_desc":
                    _data = _data.OrderByDescending(s => s.Order);
                    break;
                default:
                    _data = _data.OrderBy(s => s.ProjectName);
                    break;
            }
       
        }
    }
}

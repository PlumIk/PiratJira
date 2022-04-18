using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Examples;
using WebApplication7.Models;

namespace WebApplication7.Logic
{
    public class ProjectEmplyeeLogic
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<ProjectEmplyee> _data;
        public ProjectEmplyeeLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        //Возвращаем хранящийся лист данных в нужном нам формате
        public List<ProjectEmplyeeEx> GetData()
        {
            List<ProjectEmplyeeEx> ret = new();
            foreach (var item in _data)
            {
                ret.Add(new ProjectEmplyeeEx
                {
                     ProjectID = item.ProjectID,
                     EmployeeID = item.EmployeeID,
                     IsManager = item.IsManager ? "Yes":"No",
                     EmployeeName = item.Employee?.Patronymic,
                     Project_oneName = item.Project?.ProjectName
                });
            }
            return ret;
        }

        //Получаем единственный экземпляр, используя id
        public ProjectEmplyeeEx? GetOneById(int? idp, int? ide)
        {
            var dbOne =  _context.ProjectEmplyee.Include(p => p.Employee).Include(p => p.Project)
                        .Where (one => one.ProjectID==idp)
                        .Where(one => one.EmployeeID == ide)
                        .FirstOrDefault();
            if (dbOne == null)
            {
                return null;
            }
            ProjectEmplyeeEx ret = new()
            {
                ProjectID = dbOne.ProjectID,
                EmployeeID = dbOne.EmployeeID,
                IsManager = dbOne.IsManager ? "Yes" : "No",
                EmployeeName = dbOne.Employee?.Patronymic,
                Project_oneName = dbOne.Project?.ProjectName,
            };    
            return ret;

        }

        //Получаемвесь список из таблице и храним его для обработки
        public void InitAllData(int id)
        {
            _data = _context.ProjectEmplyee.Include(p => p.Employee).Include(p => p.Project);
            Filtre(id);
        }

        //Преобразуем данные из используемого формата в формат для базы данных и добавляем новый объект
        //Дополнительно проверяем, есть ли такой объект в базе и, если есть, ничего не делаем
        public void Add(ProjectEmplyeeEx addOne)
        {
            var one = _context.ProjectEmplyee
                        .Where(one => one.ProjectID == addOne.ProjectID)
                        .Where(one => one.EmployeeID == addOne.EmployeeID)
                        .FirstOrDefault();
            if (one == null)
            {
                var toAdd = new ProjectEmplyee
                {
                    ProjectID = addOne.ProjectID,
                    EmployeeID = addOne.EmployeeID,
                    IsManager = addOne.IsManager == "Yes"

                };
                _context.Add(toAdd);
                _context.SaveChanges();
            }
        }

        //Преобразуем данные из используемого формата в формат для базы данных и обновляем объект
        public void Update(ProjectEmplyeeEx updateOne)
        {
            var toAdd = new ProjectEmplyee
            {
                ProjectID = updateOne.ProjectID,
                EmployeeID = updateOne.EmployeeID,
                IsManager = updateOne.IsManager == "Yes"
            };
            _context.Update(toAdd);
            _context.SaveChanges();
        }

        //Удаляем объект по id
        public void DeleteConfirmed(int ide, int idp)
        {
            var deleteOne = _context.ProjectEmplyee
                        .Where(one => one.ProjectID == idp)
                        .Where(one => one.EmployeeID == ide)
                        .FirstOrDefault();
            if (deleteOne != null)
            {
                _context.ProjectEmplyee.Remove(deleteOne);
                _context.SaveChanges();
            }
        }
         //Оставляем данные только о нужном проекте
        private void Filtre(int id)
        {
            _data = _data.Where(s => s.ProjectID == id);
        }

        //Форма для удобного добавления сотрудника
        public SelectList GenEmpIdToName()
        {
            return new SelectList(_context.Employee, "EmployeeId", "Patronymic");
        }

        //Форма для удобного добавления проекта
        public SelectList GenProjIdToName()
        {
            return new SelectList(_context.Project, "ProjectId", "ProjectName");
        }

        //Форма для удобной настройки менеджера
        public SelectList GenManagerValues()
        {
            IEnumerable<ManagerEx> isManager = new List<ManagerEx>
            {
                new ManagerEx { ModelValue="Yes", ViewValue="Да" },
                new ManagerEx { ModelValue="No", ViewValue="Нет" },

            };
            return new SelectList(isManager, "ModelValue", "ViewValue");
        }

        //Получение имени проекта
        public string GetProjName(int id)
        {
            var name = _context.Project.Find(id);
            if (name == null)
            {
                return "";
            }
            return name.ProjectName;
        }

        //Проверка существования в базе данных
        public bool Exist(int ide, int idp)
        {
            var deleteOne = _context.ProjectEmplyee
                        .Where(one => one.ProjectID == idp)
                        .Where(one => one.EmployeeID == ide)
                        .FirstOrDefault();
            if (deleteOne != null)
            {
                return false;
            }
            return true;
        }

    }
}

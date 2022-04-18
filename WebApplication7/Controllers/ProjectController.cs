#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Examples;
using WebApplication7.Logic;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ProjectController : Controller
    {
        
        private readonly ProjectLogic _logic;

        public ProjectController(ApplicationDbContext context)
        {
            _logic = new ProjectLogic(context);
        }

        // GET: Project_one
        //Получаем параметры, нужные для сортировки
        public IActionResult Index(string sortOrder, string searchNameString, DateTime StartDate, DateTime EndDate,
            int MaxOrder, int MinOrder)
        {
            ViewData["ProjectNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ProjectName_desc" : "";
            ViewData["CustomerCompanyNameSortParm"] = sortOrder == "CustomerCompanyName" ? "CustomerCompanyName_desc" :
                "CustomerCompanyName";
            ViewData["PerformerCompanyNameSortParm"] = sortOrder == "PerformerCompanyName" ? "PerformerCompanyName_desc" :
                "PerformerCompanyName";
            ViewData["StartDateSortParm"] = sortOrder == "StartDate" ? "StartDate_desc" : "StartDate";
            ViewData["EndDateSortParm"] = sortOrder == "EndDate" ? "EndDate_desc" : "EndDate";
            ViewData["OrderSortParm"] = sortOrder == "Order" ? "Order_desc" : "Order";
            _logic.InitAllData();
            _logic.TakeBeforeDate(EndDate);
            _logic.TakeAfterDate(StartDate);
            _logic.TakeLess(MinOrder);
            _logic.TakeMore(MaxOrder);
            _logic.TakeWithWord(searchNameString);
            _logic.SortBy(sortOrder);
            return View(_logic.GetData());
        }

        // GET: Project_one/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_one = _logic.GetOneById(id);
            if (project_one == null)
            {
                return NotFound();
            }

            return View(project_one);
        }

        // GET: Project_one/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project_one/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectEx project_one)
        {
            if (ModelState.IsValid)
            {
                _logic.Add(project_one);
                return RedirectToAction(nameof(Index));
            }
            return View(project_one);
        }

        // GET: Project_one/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_one = _logic.GetOneById(id);
            if (project_one == null)
            {
                return NotFound();
            }
            return View(project_one);
        }

        // POST: Project_one/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProjectEx project_one)
        {
            if (id != project_one.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logic.Update(project_one);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_logic.Project_oneExists(project_one.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project_one);
        }

        // GET: Project_one/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_one = _logic.GetOneById(id);
            if (project_one == null)
            {
                return NotFound();
            }

            return View(project_one);
        }

        // POST: Project_one/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project_one = _logic.GetOneById(id);
            _logic.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

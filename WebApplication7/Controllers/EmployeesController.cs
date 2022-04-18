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
    public class EmployeesController : Controller
    {
        private readonly EmployeesLogic _logic;

        public EmployeesController(ApplicationDbContext context)
        {
            _logic = new EmployeesLogic(context);
        }

        // GET: Employees
        //Получаем параметры, нужные для сортировки
        public IActionResult Index(string sortOrder, string searchNameString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SurnameSortParm"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["PatronymicSortParm"] = sortOrder == "Patronymic" ? "patronymic_desc" : "Patronymic";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
            _logic.InitAllData();
            _logic.TakeWithWord(searchNameString);
            _logic.SortBy(sortOrder);
            return View(_logic.GetData());
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _logic.GetOneById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeEx employee)
        {
            if (ModelState.IsValid)
            {
                _logic.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _logic.GetOneById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeEx employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logic.Update(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_logic.EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _logic.GetOneById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _logic.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

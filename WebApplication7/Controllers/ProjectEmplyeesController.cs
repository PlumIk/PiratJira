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
    public class ProjectEmplyeesController : Controller
    {
        
        private readonly ProjectEmplyeeLogic _logic;
        public ProjectEmplyeesController(ApplicationDbContext context)
        {
            _logic = new ProjectEmplyeeLogic(context);
        }

        // GET: ProjectEmplyees
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _logic.InitAllData(id);
            ViewData["Title"] = "Список пиратов на квесте \"" + _logic.GetProjName(id) + "\"";

            return View(_logic.GetData());

        }


        // GET: ProjectEmplyees/Create
        //Подгружаем данные, упращающие создание
        public IActionResult Create(int id)
        {
            ViewData["Project_oneID"] = _logic.GenProjIdToName();
            ViewData["EmployeeID"] = _logic.GenEmpIdToName();
            ViewData["IsManager"] = _logic.GenManagerValues();
            return View();
        }

        // POST: ProjectEmplyees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectEmplyeeEx projectEmplyeeEx)
        {
            if (projectEmplyeeEx.EmployeeID != 0)
            {
                _logic.Add(projectEmplyeeEx);
                return RedirectToAction("Index", "ProjectEmplyees", new { id = projectEmplyeeEx.ProjectID });
            }
            ViewData["Project_oneID"] = _logic.GenProjIdToName();
            ViewData["EmployeeID"] = _logic.GenEmpIdToName();
            ViewData["IsManager"] = _logic.GenManagerValues();
            return View(projectEmplyeeEx);
        }

        // GET: ProjectEmplyees/Edit/5
        public IActionResult Edit(int? ide, int? idp)
        {
            if (ide == null || idp == null)
            {
                return NotFound();
            }

            var projectEmplyee = _logic.GetOneById(ide, idp);
            if (projectEmplyee == null)
            {
                return NotFound();
            }
            ViewData["IsManager"] = _logic.GenManagerValues();
            return View(projectEmplyee);
        }

        // POST: ProjectEmplyees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectEmplyeeEx projectEmplyeeEx)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _logic.Update(projectEmplyeeEx);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_logic.Exist(projectEmplyeeEx.EmployeeID, projectEmplyeeEx.ProjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ProjectEmplyees", new { id = projectEmplyeeEx.ProjectID });
            }
            ViewData["IsManager"] = _logic.GenManagerValues();
            return View(projectEmplyeeEx);
        }

        // GET: ProjectEmplyees/Delete/5
        public IActionResult Delete(int? ide, int? idp)
        {
            if (ide == null || idp == null)
            {
                return NotFound();
            }

            _logic.DeleteConfirmed((int)ide, (int)idp);

            return RedirectToAction("Index", "ProjectEmplyees", new { id = idp });
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCMS.Controllers
{

    [Authorize(Roles = "WFM, Admin, Human Resources")]
    public class DepartmentController : Controller
    {
        private readonly CCMSContext _context;

        public DepartmentController(CCMSContext context)
        {
            _context = context;
            if (_context.Departments.AsNoTracking().ToList().Count() == 0)
            {
                _context.Departments.Add(new Department
                {
                    Name = "Temp",
                    Description = "Temp"
                });
                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Department";
            List<Department> departments = _context.Departments.AsNoTracking().ToList();
            return View(departments);
        }

        [HttpPost]
        public IActionResult Add(AddDepartmentViewModel addDepartmentViewModel)
        {
            if (addDepartmentViewModel.Name != null)
            {
                Department newDepartment = new Department
                {
                    Name = addDepartmentViewModel.Name,
                    Description = addDepartmentViewModel.Description
                };
                _context.Departments.Add(newDepartment);
                _context.SaveChanges();
                return Redirect("/Department/View/" + newDepartment.Id);
            }
            else
            {
                return View("Add");
            }
        }

        public async Task<IActionResult> View(long id)
        {
            Department department = await _context.Departments.AsNoTracking().SingleAsync(d => d.Id == id);

            return View("View", department);
        }

        //[Route("{controller}/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            Department department = await _context.Departments.AsNoTracking().SingleAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        //[Route("{controller}/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(long id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                if (department.Name != "")
                {
                    _context.Entry(department).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                } else
                {
                    return View(department);
                }
            }
            return Redirect("/Department/View/" + id);
        }

        public IActionResult Add()
        {
            return View("Add");
        }

    }
}
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
            ViewDepartmentViewModel viewDepartmentViewModel = new ViewDepartmentViewModel
            {
                Department = department,
                AllotmentYears = _context.Allotments.AsNoTracking()
                                            .Where(a => a.DepartmentID == id)
                                            //.Where(a => a.TDate.Year >= DateTime.Now.Year)
                                            .OrderBy(a => a.Date.Year)
                                            .Select(a => a.Date.Year)
                                            .Distinct()
                                            .ToList()
            };

            return View("View", viewDepartmentViewModel);
        }

        public IActionResult ViewAllotments(long id, long year)
        {
            //Department department = await _context.Departments.AsNoTracking().SingleAsync(d => d.Id == id);
            IList<Allotment> allotments = _context.Allotments.AsNoTracking()
                                                    .Where(a => a.DepartmentID == id)
                                                    .Where(a => a.Date.Year == year)
                                                    .OrderBy(a => a.Date)
                                                    .ToList();

            return View(allotments);


        }

        public async Task<IActionResult> Edit(long id)
        {
            Department department = await _context.Departments.AsNoTracking().SingleAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

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
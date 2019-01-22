﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    [Authorize]
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
            List<Department> departments = _context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(AddDepartmentViewModel addDepartmentViewModel)
        {
            if (addDepartmentViewModel.Name != null)
            {
                Department newDepartment = new Department
                {
                    Name = addDepartmentViewModel.Name
                };
                _context.Departments.Add(newDepartment);
                _context.SaveChanges();
                return Redirect("/Department");
            }
            else
            {
                return View("Add");
            }
        }
    }
}
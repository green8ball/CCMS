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
    public class ManagementUnitController : Controller
    {
        private readonly CCMSContext _context;

        public ManagementUnitController(CCMSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "ManagmentUnit";
            List<ManagementUnit> ManagmentUnits = _context.ManagementUnits.AsNoTracking().ToList();
            return View(ManagmentUnits);
        }

        [HttpPost]
        public IActionResult Add(AddManagementUnitViewModel addManagmentUnitViewModel)
        {
            if (addManagmentUnitViewModel.Name != null)
            {
                ManagementUnit newManagmentUnit = new ManagementUnit
                {
                    Name = addManagmentUnitViewModel.Name,
                    Description = addManagmentUnitViewModel.Description
                };
                _context.ManagementUnits.Add(newManagmentUnit);
                _context.SaveChanges();
                return Redirect("/ManagmentUnit/View/" + newManagmentUnit.Id);
            }
            else
            {
                return View("Add");
            }
        }

        public async Task<IActionResult> View(long id)
        {
            ManagementUnit ManagmentUnit = await _context.ManagementUnits.AsNoTracking().SingleAsync(d => d.Id == id);
            ViewManagementUnitViewModel viewManagmentUnitViewModel = new ViewManagementUnitViewModel
            {
                ManagementUnit = ManagmentUnit,
                AllotmentYears = _context.Allotments.AsNoTracking()
                                            .Where(a => a.ManagementUnitID == id)
                                            //.Where(a => a.TDate.Year >= DateTime.Now.Year)
                                            .OrderBy(a => a.Date.Year)
                                            .Select(a => a.Date.Year)
                                            .Distinct()
                                            .ToList()
            };

            return View("View", viewManagmentUnitViewModel);
        }

        public IActionResult ViewAllotments(long id, long year)
        {
            //ManagmentUnit ManagmentUnit = await _context.ManagmentUnits.AsNoTracking().SingleAsync(d => d.Id == id);
            IList<Allotment> allotments = _context.Allotments.AsNoTracking()
                                                    .Where(a => a.ManagementUnitID == id)
                                                    .Where(a => a.Date.Year == year)
                                                    .OrderBy(a => a.Date)
                                                    .ToList();

            return View(allotments);


        }

        public async Task<IActionResult> Edit(long id)
        {
            ManagementUnit ManagmentUnit = await _context.ManagementUnits.AsNoTracking().SingleAsync(d => d.Id == id);

            if (ManagmentUnit == null)
            {
                return NotFound();
            }

            return View(ManagmentUnit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, ManagementUnit ManagmentUnit)
        {
            if (id != ManagmentUnit.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                if (ManagmentUnit.Name != "")
                {
                    _context.Entry(ManagmentUnit).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                } else
                {
                    return View(ManagmentUnit);
                }
            }
            return Redirect("/ManagmentUnit/View/" + id);
        }

        public IActionResult Add()
        {
            return View("Add");
        }

    }
}
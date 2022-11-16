using Laba4.Data;
using Laba4.Models;
using Laba4.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly Context _context;
        public SpecializationController(Context context)
        {
            _context = context;
        }
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 256)]
        public ActionResult Index(SortState sortSpecialization, string searchDepartmentName, string searchSpecializationName)
        {
            ViewBag.SearchDepartmentName = searchDepartmentName;
            ViewBag.SearchSpecializationName = searchSpecializationName;
            IEnumerable<SpecializationView> specializationViews = from s in _context.Specializations.ToList()
                                                    join d in _context.Departments
                                                    on s.DepartmentId equals d.Id
                                                    select new SpecializationView
                                                    {
                                                        Id = s.Id,
                                                        Name = s.Name,
                                                        Department = d.Name
                                                    };
            specializationViews = Search(specializationViews, searchDepartmentName, searchSpecializationName);
            specializationViews = Sort(specializationViews, sortSpecialization);
            return View(specializationViews.ToList());
        }
        private IEnumerable<SpecializationView> Sort(IEnumerable<SpecializationView> specializationViews, SortState sortSpecialization)
        {
            ViewData["DepartmentName"] = sortSpecialization == SortState.DepartmentNameAsc ? SortState.DepartmentNameDesc : SortState.DepartmentNameAsc;
            ViewData["SpecializationName"] = sortSpecialization == SortState.SpecializationNameAsc ? SortState.SpecializationNameDesc : SortState.SpecializationNameAsc;
            specializationViews = sortSpecialization switch
            {
                SortState.SpecializationNameAsc => specializationViews.OrderBy(s => s.Name),
                SortState.SpecializationNameDesc => specializationViews.OrderByDescending(s => s.Name),
                SortState.DepartmentNameAsc => specializationViews.OrderBy(s => s.Department),
                _ => specializationViews.OrderByDescending(s => s.Department)
            };
            return specializationViews;
        }
        private IEnumerable<SpecializationView> Search(IEnumerable<SpecializationView> specializationViews, string searchDepartmentName, string searchSpecializationName)
        {
            if (!string.IsNullOrEmpty(searchDepartmentName))
            {
                specializationViews = specializationViews.Where(s => s.Department.Contains(searchDepartmentName));
            }
            if (!string.IsNullOrEmpty(searchSpecializationName))
            {
                specializationViews = specializationViews.Where(s => s.Name.Contains(searchSpecializationName));
            }
            return specializationViews;
        }
    }
}

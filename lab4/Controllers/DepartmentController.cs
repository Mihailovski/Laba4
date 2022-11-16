using Laba4.Data;
using Laba4.Models;
using Laba4.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Context _context;
        public DepartmentController(Context context)
        {
            _context = context;
        }
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 256)]
        public ActionResult Index(SortState sortDepartment, string searchDepartmentName, string searchFacultyName)
        {
            ViewBag.SearchDepartmentName = searchDepartmentName;
            ViewBag.SearchFacultyName = searchFacultyName;
            IEnumerable<DepartmentView> departmentViews = from d in _context.Departments.ToList()
                                                    join f in _context.Faculties
                                                    on d.FacultyId equals f.Id
                                                    select new DepartmentView
                                                    {
                                                        Id = d.Id,
                                                        Name = d.Name,
                                                        IsReleasing = d.IsReleasing ? "Выпускающая" : "Общеобразовательная",
                                                        Faculty = f.Name
                                                    };
            departmentViews = Search(departmentViews, searchDepartmentName, searchFacultyName);
            departmentViews = Sort(departmentViews, sortDepartment);
            return View(departmentViews.ToList());
        }
        private IEnumerable<DepartmentView> Sort(IEnumerable<DepartmentView> departmentViews, SortState sortDepartment)
        {
            ViewData["DepartmentSort"] = sortDepartment == SortState.DepartmentNameAsc ? SortState.DepartmentNameDesc : SortState.DepartmentNameAsc;
            ViewData["FacultySort"] = sortDepartment == SortState.FacultyNameAsc ? SortState.FacultyNameDesc : SortState.FacultyNameAsc;
            departmentViews = sortDepartment switch
            {
                SortState.DepartmentNameAsc => departmentViews.OrderBy(d => d.Name),
                SortState.DepartmentNameDesc => departmentViews.OrderByDescending(d => d.Name),
                SortState.FacultyNameAsc => departmentViews.OrderBy(d => d.Faculty),
                _ => departmentViews.OrderByDescending(d => d.Faculty)
            };
            return departmentViews;
        }
        private IEnumerable<DepartmentView> Search(IEnumerable<DepartmentView> departmentViews, string searchDepartmentName, string searchFacultyName)
        {
            if (!string.IsNullOrEmpty(searchDepartmentName))
            {
                departmentViews = departmentViews.Where(d => d.Name.Contains(searchDepartmentName));
            }
            if (!string.IsNullOrEmpty(searchFacultyName))
            {
                departmentViews = departmentViews.Where(d => d.Faculty.Contains(searchFacultyName));
            }
            return departmentViews;
        }
    }
}

using Laba4.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Laba4.Controllers
{
    public class FacultyController : Controller
    {
        private readonly Context _context;
        public FacultyController(Context context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View(_context.Faculties.ToList());
        }
    }
}

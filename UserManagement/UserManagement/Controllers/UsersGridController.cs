using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    public class UsersGridController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

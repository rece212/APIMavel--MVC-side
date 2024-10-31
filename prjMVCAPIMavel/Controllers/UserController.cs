using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjMVCAPIMavel.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "User")] // Only allow users
                                    // with the Admin role
        public IActionResult UserDashboard()
        {
            return View(); 
        }
    }
}

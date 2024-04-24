using Microsoft.AspNetCore.Mvc;

namespace LabASPCORE.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}

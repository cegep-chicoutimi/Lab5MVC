using LabASPCORE.Areas.Admin.ViewModels;
using LabASPCORE.DataAccessLayer;
using LabASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabASPCORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {

        public IActionResult List()
        {
            DAL dal = new DAL();
            List<Reservation> listeReservations = dal.ReservationFact.GetAll();
            return View(listeReservations);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Reservation? reservation = dal.ReservationFact.Get(id);

                if (reservation != null)
                {
                    ReservationDeleteVM viewModel = new ReservationDeleteVM(reservation, dal.MenuChoixFact.Get(reservation.IdMenuChoix));

                    return View(viewModel);
                }
            }

            return View("AdminMessage", new AdminMessageVM("L'identifiant du produit est introuvable."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                new DAL().ReservationFact.Delete(id);
            }

            return RedirectToAction("List");
        }
    }
}

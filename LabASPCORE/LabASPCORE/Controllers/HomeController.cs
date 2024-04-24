using LabASPCORE.DataAccessLayer;
using LabASPCORE.Models;
using LabASPCORE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LabASPCORE.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DAL dal = new DAL();
            IndexVM viewModel = new IndexVM(
                dal.ReservationFact.CreateEmpty(),
                dal.MenuChoixFact.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexVM viewModel)
        {
            if (viewModel != null && viewModel.Reservation != null)
            {
                DAL dal = new DAL();

                Reservation? existingReservation = dal.ReservationFact.Get(viewModel.Reservation.Id);
                if (existingReservation != null)
                {
                    // Il est possible d'ajouter une erreur personnalisée.
                    // Le premier paramètre est la propriété touchée (à partir du viewModel ici)

                    ModelState.AddModelError("Description", "Le nom du menu existe déjà.");
                }

                if (!ModelState.IsValid)
                {
                    // Si le modèle n'est pas valide, on retourne à la vue CreateEdit où les messages seront affichés.
                    // Le ViewModèle reçu en POST n'est pas complet (seulement les info dans le <form> sont conservées),
                    // il faut donc réaffecter les Catégories.

                    return View("Index", viewModel);
                }
                dal.ReservationFact.Save(viewModel.Reservation);
            }
            return RedirectToAction("Details", "Reservation", new{ id = viewModel.Reservation.Id });
        }
    }
}

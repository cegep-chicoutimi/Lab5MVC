using Microsoft.AspNetCore.Mvc;
using LabASPCORE.DataAccessLayer;
using LabASPCORE.Models;
using LabASPCORE.Areas.Admin.ViewModels;

namespace LabASPCORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuChoiceController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();
            List<MenuChoix> listeMenus = dal.MenuChoixFact.GetAll();
            return View(listeMenus);
        }

        public IActionResult Create()
        {
            DAL dal = new DAL();
            MenuChoix menuChoix = dal.MenuChoixFact.CreateEmpty();
            return View("CreateEdit", menuChoix);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuChoix Model)
        {
            if (Model != null && Model.Description != null)
            {
                DAL dal = new DAL();

                MenuChoix? existingChoice = dal.MenuChoixFact.GetByDesc(Model.Description);
                if (existingChoice != null)
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

                    return View("CreateEdit", Model);
                }

                dal.MenuChoixFact.Save(Model);
            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                MenuChoix? choix = dal.MenuChoixFact.Get(id);

                if (choix != null)
                {

                    return View("CreateEdit", choix);
                }
            }

            return View("AdminMessage", new AdminMessageVM("L'identifiant du produit est introuvable."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MenuChoix Model)
        {
            if (Model != null && Model.Description != null)
            {
                DAL dal = new DAL();

                MenuChoix? existingChoice = dal.MenuChoixFact.GetByDesc(Model.Description);
                if (existingChoice != null && existingChoice.Id == Model.Id)
                {
                    ModelState.AddModelError("Description", "Le code de produit existe déjà.");
                }

                if (!ModelState.IsValid)
                {
                    return View("CreateEdit", Model);
                }

                dal.MenuChoixFact.Save(Model);
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                MenuChoix? choix = dal.MenuChoixFact.Get(id);

                if (choix != null)
                {

                    return View(choix);
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
                new DAL().MenuChoixFact.Delete(id);
            }

            return RedirectToAction("List");
        }
    }
}

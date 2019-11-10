using Mark.Models.Class;
using Mark.Models.InterFace;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mark.Controllers
{
    public class StoreController : Controller
    {
        private readonly ICompaniesService _companyService;
        private readonly IStoresService _storesService;

        public StoreController(ICompaniesService companySevice, IStoresService storesService)
        {
            _companyService = companySevice;
            _storesService = storesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public JsonResult GetStore()
        //{
        //    var store = _companyService.AllCompanies();

        //    return Json(store);
        //}


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _storesService.FindStore((int)id);

            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Stores stores, int? EditId)
        {
            if (ModelState.IsValid)
            {
                _storesService.UpDateStore(stores, EditId);
                return RedirectToAction(nameof(Details), "Company", new { id = EditId });
            }

            return View(stores);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _storesService.FindStore((int)id);

            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? DelId)
        {
            if (DelId != null)
            {
                _storesService.DeleteStore((int)DelId);
                return RedirectToAction(nameof(Index), "Company"/*, new { Id = DelId }*/);
            }
            return Content("");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _storesService.FindStore((int)id);

            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }
    }
}
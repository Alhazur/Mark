using Mark.Models;
using Mark.Models.Class;
using Mark.Models.InterFace;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Mark.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompaniesService _companyService;
        private readonly IStoresService _storesService;

        public CompanyController(ICompaniesService companySevice, IStoresService storesService)
        {
            _companyService = companySevice;
            _storesService = storesService;
        }

        public IActionResult Index()
        {
            return View(_companyService.AllCompanies());
        }

        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCompany(Companies company)
        {
            if (ModelState.IsValid)
            {
                company = _companyService.CreateCompany(
                company.Name,
                company.OrganizationNumber,
                company.Note);

                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IActionResult CreateStore(int companyId)//den variable namn måste skrivas i asp-route-(när - companyId)="@Model.Id är class id" 
        {
            var store = new StoreViewModel
            {
                CompanyId = companyId
            };

            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStore(Stores stores, int newId)//Post elle input deras id  har new variable, inte samma som get id namn name="newId"
        {
            if (ModelState.IsValid)
            {
                _storesService.CreateStore(stores, newId);
                return RedirectToAction(nameof(Details), "Company", new { id = newId });
            }

            return View(stores);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = _companyService.FindCompany((int)id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Companies companies)
        {
            if (ModelState.IsValid)
            {
                _companyService.UpDateCompany(companies);
                return RedirectToAction(nameof(Index));
            }
            return View(companies);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                _companyService.DeleteCompany((int)id);
                _storesService.DeleteStore((int)id);
                return RedirectToAction(nameof(Index));
            }
            return Content("");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = _companyService.FindCompany((int)id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
    }
}
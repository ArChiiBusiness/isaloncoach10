using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL;
using BLL.Interfaces;

namespace isaloncoach10.Controllers
{
    public class FormController : Controller
    {
        IFormService _formService;
        private ISalonService _salonService;

        public FormController(IFormService formService, ISalonService salonService)
        {
            _formService = formService;
            _salonService = salonService;
        }

        // GET: FormController
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: FormController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FormController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormController/Create
        [HttpPost]
        public async Task<IActionResult> Submit(ActualBOL data)
        {

            // Add salon if not exists
            bool salonExists = await _salonService.SalonExists(data.SalonName);
            Guid salonId = Guid.NewGuid();

            if (salonExists)
            {
                salonId = await _salonService.GetSalonIdByName(data.SalonName);
            }
            else
            {
                SalonBOL salon = new SalonBOL
                {
                    Name = data.SalonName,
                    ContactName = data.ContactName,
                    Email = data.Email
                };
                salonId = await _salonService.AddSalon(salon);
            }

            Guid actualId = await _salonService.AddActualData(data, salonId);
            return RedirectToAction("ThankYou");
        }


        // POST: FormController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FormController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            await _formService.DeleteResponse(id);
            return View("Index");
        }

        // POST: FormController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ThankYou()
        {
            return View();
        }
    }
}

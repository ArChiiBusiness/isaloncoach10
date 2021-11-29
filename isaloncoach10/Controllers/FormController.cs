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

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        // GET: FormController
        public async Task<ActionResult> Submit()
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
        public async Task<ActionResult> Submit(FormDataBOL data)
        {
            var submitResult = await _formService.SubmitData(data);
            if (submitResult == true)
            {
                return View();
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: FormController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
        public ActionResult Delete(int id)
        {
            return View();
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
    }
}

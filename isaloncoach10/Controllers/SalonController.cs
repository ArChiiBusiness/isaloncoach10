using BLL.Interfaces;
using BOL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;

namespace isaloncoach10.Controllers
{
    public class SalonController : Controller
    {
        private ISalonService _salonService;
        private readonly IWebHostEnvironment _environment;

        public SalonController(IFormService formService, ISalonService salonService, IWebHostEnvironment environment)
        {
            _salonService = salonService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _salonService.GetAll());
        }

        [HttpGet]
        [Route("/salon/add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSalon(SalonBOL salon)
        {
            Guid salonId = await _salonService.AddSalon(salon);
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("/salon/{salonId}/edit")]
        public async Task<IActionResult> Edit(Guid salonId)
        {
            return View(await _salonService.GetSalonById(salonId));
        }

        [HttpPost]
        [Route("/salon/{salonId}/edit")]
        public async Task<IActionResult> EditSalon([FromForm] SalonBOL salon, [FromRoute] Guid salonId)
        {
            salon.Id = salonId;
            bool updateResult = await _salonService.UpdateSalon(salon);
            return RedirectToAction("index");
        }

        [Route("/salon/{salonId}/delete")]
        public async Task<IActionResult> Delete(Guid salonId)
        {
            await _salonService.DeleteSalon(salonId);
            return RedirectToAction("index");
        }

        [Route("/salon/{salonId}/actuals")]
        public async Task<IActionResult> Actuals(Guid salonId)
        {
            ViewData["SalonName"] = await _salonService.GetSalonNameById(salonId);
            return View("Actuals", await _salonService.GetActuals(salonId));
        }

        [Route("/actual/{actualId}/details")]
        public async Task<IActionResult> ActualDetails(Guid actualId)
        {
            return View("../Actual/Details", await _salonService.GetActualById(actualId));
        }

        [Route("/actual/{actualId}/delete")]
        public async Task<IActionResult> DeleteActual(Guid actualId)
        {
            Guid salonId = (await _salonService.GetActualById(actualId)).SalonId;
            await _salonService.DeleteActual(actualId);
            return RedirectToAction("Actuals", new { salonId = salonId });
            //return View("Actuals", await _salonService.GetActuals(salonId));
        }

        [Route("/salon/{salonId}/targets")]
        public async Task<IActionResult> Targets(Guid salonId)
        {
            ViewData["SalonName"] = await _salonService.GetSalonNameById(salonId);
            return View("Targets", await _salonService.GetTargets(salonId));
        }

        [HttpGet]
        [Route("/salon/{salonId}/addtarget")]
        public async Task<IActionResult> AddTarget(Guid salonId)
        {
            return View("AddTarget");
        }

        [HttpPost]
        [Route("/salon/{salonId}/addtarget")]
        public async Task<IActionResult> AddTargetSubmit(TargetBOL target)
        {
            await _salonService.AddTargetData(target, Guid.Parse(RouteData.Values["salonId"].ToString()));
            return RedirectToAction("Targets", new { salonId = RouteData.Values["salonId"] });
        }

        [HttpGet]
        [Route("/target/{targetId}/edit")]
        public async Task<IActionResult> EditTarget(Guid targetId)
        {
            return View("../target/edit", await _salonService.GetTargetById(targetId));
        }

        [HttpPost]
        [Route("/target/{targetId}/edit")]
        public async Task<IActionResult> EditTarget([FromForm] TargetBOL target, [FromRoute] Guid targetId)
        {
            target.Id = targetId;
            await _salonService.UpdateTarget(target);
            Guid salonId = (await _salonService.GetTargetById(targetId)).SalonId;
            return RedirectToAction("Targets", new { salonId = salonId });
        }

        [Route("/target/{targetId}/delete")]
        public async Task<IActionResult> DeleteTarget(Guid targetId)
        {
            Guid salonId = (await _salonService.GetTargetById(targetId)).SalonId;
            await _salonService.DeleteTarget(targetId);
            return RedirectToAction("Targets", new { salonId = salonId });
        }

        [Route("/target/{targetId}/details")]
        public async Task<IActionResult> TargetDetails(Guid targetId)
        {
            return View("../Target/Details", await _salonService.GetTargetById(targetId));
        }

        [Route("/actual/{actualId}/document")]
        public async Task<IActionResult> ActualDocument(Guid actualId)
        {
            ActualBOL actual = await _salonService.GetActualById(actualId);
            DateTime currentDate = new DateTime(actual.Year, actual.Month, 1);
            DateTime prevDate = currentDate.AddMonths(-1);
            int prevYear = prevDate.Year;
            int prevMonth = prevDate.Month;
            ActualBOL previousActual = await _salonService.GetActualByMonthYear(actual.SalonId, prevYear, prevMonth);
            TargetBOL target = await _salonService.GetTargetByYear(actual.SalonId, actual.Year);

            Guid nId = Guid.NewGuid();
            string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            string path = Path.Combine(_environment.WebRootPath, "Template.docx");
            string nPath = Path.Combine(_environment.WebRootPath, $"Template_{nId.ToString()}.docx");
            string tmplPath = Path.Combine(_environment.WebRootPath, "word_template.txt");
            string tmplText;
            FileStream templateStream = new FileStream(tmplPath, FileMode.Open);
            using (StreamReader templateReader = new StreamReader(templateStream))
            {
                tmplText = templateReader.ReadToEnd();
            }

            string docText = tmplText;

            docText = docText.Replace("{{salon_name}}", actual.SalonName.ToString());
            docText = docText.Replace("{{name}}", actual.ContactName.ToString());
            docText = docText.Replace("{{month}}", actual.Month.ToString());
            docText = docText.Replace("{{year}}", actual.Year.ToString());

            // Actuals
            docText = docText.Replace("{{tot_takings_a}}", actual.TotalTakings.ToString());
            docText = docText.Replace("{{retail_a}}", actual.RetailMonth.ToString());
            docText = docText.Replace("{{wage_bill_a}}", actual.WageBillMonth.ToString());
            docText = docText.Replace("{{client_visits_a}}", actual.ClientVisitsMonth.ToString());
            docText = docText.Replace("{{rebooks_a}}", actual.RebooksMonth.ToString());
            docText = docText.Replace("{{client_visits_year_a}}", actual.ClientVisitsLastYear.ToString());
            docText = docText.Replace("{{individual_clients_year_a}}", actual.IndividualClientVisitsLastYear.ToString());
            docText = docText.Replace("{{new_clients_a}}", actual.NewClientsMonth.ToString());
            docText = docText.Replace("{{clients_in_db_a}}", actual.TotalClientsInDatabase.ToString());

            // Targets
            if (target != null)
            {
                docText = docText.Replace("{{tot_takings_t}}", target.TotalTakings.ToString());
                docText = docText.Replace("{{retail_t}}", target.RetailMonth.ToString());
                docText = docText.Replace("{{wage_bill_t}}", target.WageBillMonth.ToString());
                docText = docText.Replace("{{client_visits_t}}", target.ClientVisitsMonth.ToString());
                docText = docText.Replace("{{rebooks_t}}", target.RebooksMonth.ToString());
                docText = docText.Replace("{{client_visits_year_t}}", target.ClientVisitsLastYear.ToString());
                docText = docText.Replace("{{individual_clients_year_t}}", target.IndividualClientVisitsLastYear.ToString());
                docText = docText.Replace("{{new_clients_t}}", target.NewClientsMonth.ToString());
                docText = docText.Replace("{{clients_in_db_t}}", target.TotalClientsInDatabase.ToString());
            }
            else
            {
                docText = docText.Replace("{{tot_takings_t}}", "-");
                docText = docText.Replace("{{retail_t}}", "-");
                docText = docText.Replace("{{wage_bill_t}}", "-");
                docText = docText.Replace("{{client_visits_t}}", "-");
                docText = docText.Replace("{{rebooks_t}}", "-");
                docText = docText.Replace("{{client_visits_year_t}}", "-");
                docText = docText.Replace("{{individual_clients_year_t}}", "-");
                docText = docText.Replace("{{new_clients_t}}", "-");
                docText = docText.Replace("{{clients_in_db_t}}", "-");
            }

            // Result
            if (target != null)
            {
                docText = docText.Replace("{{tot_takings_r}}", $"{Math.Round(actual.TotalTakings / target.TotalTakings * 100, 2)}%");
                docText = docText.Replace("{{retail_r}}", $"{Math.Round(actual.RetailMonth / target.RetailMonth * 100, 2)}%");
                docText = docText.Replace("{{wage_bill_r}}", $"{Math.Round(actual.WageBillMonth / target.WageBillMonth * 100, 2)}%");
                docText = docText.Replace("{{client_visits_r}}", $"{Math.Round(actual.NewClientsMonth / target.NewClientsMonth * 100, 2)}%");
                docText = docText.Replace("{{rebooks_r}}", $"{Math.Round(actual.RebooksMonth / target.RebooksMonth * 100, 2)}%");
                docText = docText.Replace("{{client_visits_year_r}}", $"{Math.Round(actual.ClientVisitsLastYear / target.ClientVisitsLastYear * 100, 2)}%");
                docText = docText.Replace("{{individual_clients_year_r}}", $"{Math.Round(actual.IndividualClientVisitsLastYear / target.IndividualClientVisitsLastYear * 100, 2)}%");
                docText = docText.Replace("{{new_clients_r}}", $"{Math.Round(actual.NewClientsMonth / target.NewClientsMonth * 100, 2)}%");
                docText = docText.Replace("{{clients_in_db_r}}", $"{Math.Round(actual.TotalClientsInDatabase / target.TotalClientsInDatabase * 100, 2)}%");
            }
            else
            {
                docText = docText.Replace("{{tot_takings_r}}", "-");
                docText = docText.Replace("{{retail_r}}", "-");
                docText = docText.Replace("{{wage_bill_r}}", "-");
                docText = docText.Replace("{{client_visits_r}}", "-");
                docText = docText.Replace("{{rebooks_r}}", "-");
                docText = docText.Replace("{{client_visits_year_r}}", "-");
                docText = docText.Replace("{{individual_clients_year_r}}", "-");
                docText = docText.Replace("{{new_clients_r}}", "-");
                docText = docText.Replace("{{clients_in_db_r}}", "-");
            }

            System.IO.File.Copy(path, nPath, false);
            using (WordprocessingDocument document = WordprocessingDocument.Open(nPath, true))
            {
                using (StreamWriter sw = new StreamWriter(document.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
                //using (StreamReader sr = new StreamReader(document.MainDocumentPart.GetStream()))
                //{
                //    docText = sr.ReadToEnd();
                //    return Content(docText);
                //}
            };

            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string fName = $"{actual.SalonName}_{DateTime.Now.Year}.docx";
            MemoryStream output_stream = new MemoryStream();

            using (FileStream fStream = System.IO.File.Open(nPath, FileMode.Open))
            {
                fStream.CopyTo(output_stream);
                fStream.Close();
            };

            System.IO.File.Delete(nPath);
            output_stream.Seek(0, SeekOrigin.Begin);

            return File(output_stream, contentType, fName);
        }

        [Route("/salon/document")]
        public async Task<IActionResult> GetDocumentData()
        {
            string path = Path.Combine(_environment.WebRootPath, "Template.docx");
            string nPath = Path.Combine(_environment.WebRootPath, $"Template_{Guid.NewGuid().ToString()}.docx");

            System.IO.File.Copy(path, nPath, false);
            using (WordprocessingDocument document = WordprocessingDocument.Open(nPath, true))
            {
                using (StreamReader sr = new StreamReader(document.MainDocumentPart.GetStream()))
                {
                    string docText = sr.ReadToEnd();
                    return Content(docText);
                }
            };
        }
    }
}

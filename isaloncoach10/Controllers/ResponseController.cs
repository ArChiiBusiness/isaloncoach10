using BLL.Interfaces;
using BOL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;

namespace isaloncoach10.Controllers
{
    public class ResponseController : Controller
    {
        IFormService _formService;
        private readonly IWebHostEnvironment _environment;

        public ResponseController(IFormService formService, IWebHostEnvironment environment)
        {
            _environment = environment;
            _formService = formService;
        }

        public async Task<IActionResult> Index()
        {
            List<FormDataBOL> data = await _formService.GetFormDataAll();
            return View(data);
        }

        [HttpGet]
        [Route("response/{id}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _formService.DeleteResponse(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("response/{id}/details")]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _formService.GetFormData(id));
        }

        [HttpGet]
        [Route("response/{id}/document")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Document(Guid id)
        {
            FormDataBOL data = await _formService.GetFormData(id);

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

            docText = docText.Replace("{{salon_name}}", data.SalonName.ToString());
            docText = docText.Replace("{{name}}", data.Name.ToString());
            docText = docText.Replace("{{month}}", Months[int.Parse(data.Month)]);
            docText = docText.Replace("{{year}}", data.Timestamp.Year.ToString());

            docText = docText.Replace("{{tot_takings_t}}", data.TargetMonthUSD.ToString());
            docText = docText.Replace("{{tot_takings_a}}", data.TotalMonthlyTakings.ToString());
            docText = docText.Replace("{{tot_takings_r}}", $"{(Math.Round((data.TotalMonthlyTakings / data.TargetMonthUSD) * 100, 2)).ToString()}%");

            docText = docText.Replace("{{retail_perc_t}}", "0");
            docText = docText.Replace("{{retail_perc_a}}", "0");
            docText = docText.Replace("{{retaild_perc_r}}", "0");

            docText = docText.Replace("{{client_visits_t}}", data.TargetClientsMonth.ToString());
            docText = docText.Replace("{{client_visits_a}}", data.ClientVisitsMonth.ToString());
            docText = docText.Replace("{{client_visits_r}}", $"{(Math.Round((double.Parse(data.ClientVisitsMonth.ToString()) / double.Parse(data.TargetClientsMonth.ToString())) * 100, 2)).ToString()}%");

            docText = docText.Replace("{{new_clients_t}}", "0");
            docText = docText.Replace("{{new_clients_a}}", data.NewClientsMonth.ToString());
            docText = docText.Replace("{{new_clients_r}}", "0");

            docText = docText.Replace("{{avg_bill_t}}", "0");
            docText = docText.Replace("{{avg_bill_a}}", "0");
            docText = docText.Replace("{{avg_bill_r}}", "0");

            docText = docText.Replace("{{year_takings_t}}", "0");
            docText = docText.Replace("{{year_takings_a}}", data.PastYearTotalTakings.ToString());
            docText = docText.Replace("{{year_takings_r}}", "0");

            docText = docText.Replace("{{wage_perc_t}}", "0");
            docText = docText.Replace("{{wage_perc_a}}", "0");
            docText = docText.Replace("{{wage_perc_r}}", "0");

            docText = docText.Replace("{{clients_in_db_t}}", "0");
            docText = docText.Replace("{{clients_in_db_a}}", data.TotalClientsInDatabase.ToString());
            docText = docText.Replace("{{clients_in_db_r}}", "0");

            docText = docText.Replace("{{avg_client_visits_year_t}}", "0");
            docText = docText.Replace("{{avg_client_visits_year_a}}", "0");
            docText = docText.Replace("{{avg_client_visits_year_r}}", "0");

            docText = docText.Replace("{{weeks_between_appts_t}}", "0");
            docText = docText.Replace("{{weeks_between_appts_a}}", "0");
            docText = docText.Replace("{{weeks_between_appts_r}}", "0");

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
            string fName = $"{data.SalonName}_{DateTime.Now.Year}.docx";
            MemoryStream output_stream = new MemoryStream();

            using (FileStream fStream = System.IO.File.Open(nPath, FileMode.Open))
            {
                fStream.CopyTo(output_stream);
                fStream.Close();
            };

            System.IO.File.Delete(nPath);
            output_stream.Seek(0, SeekOrigin.Begin);

            return File(output_stream, contentType, fName);

            //using var temp_file_stream = new MemoryStream();
            //document.Save(temp_file_stream, FormatType.Docx);
            //temp_file_stream.Seek(0, SeekOrigin.Begin);

            //MemoryStream output_stream = new MemoryStream(temp_file_stream.ToArray());
            //output_stream.Seek(0, SeekOrigin.Begin);

            //string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //string fName = $"{data.SalonName}_{DateTime.Now.Year}.docx";

            //return File(output_stream, contentType, fName);
        }

    }
}

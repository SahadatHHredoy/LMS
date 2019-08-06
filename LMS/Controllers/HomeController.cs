using LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.Windows.Forms;
using System.Diagnostics;


namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HomeModel model)
        {
            model.Date = string.IsNullOrEmpty(model.Date) ? DateTime.Now.DateFormat() : model.Date;
            model.LoadData();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ConvertHtmlToPdf(string date)
        {
            HtmlToPdf converter = new HtmlToPdf();
            var leftPartUrl = String.Format("{0}://{1}:{2}", Request.RequestContext.HttpContext.Request.Url.Scheme, Request.RequestContext.HttpContext.Request.Url.Host, Request.RequestContext.HttpContext.Request.Url.Port);
            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;



            // footer settings //
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 50;
        
         
       
            // convert url //
            string url = Url.Action("GeneratePdfOrPrint", new { date = date });
            url = String.Format("{1}{0}", url, leftPartUrl);
            PdfDocument doc = converter.ConvertUrl(url);
            MemoryStream stream = new MemoryStream();

            // set the document properties //
            HtmlToPdfResult result = converter.ConversionResult;
            doc.DocumentInformation.Title = result.WebPageInformation.Title;
            doc.DocumentInformation.Subject = result.WebPageInformation.Description;
            doc.DocumentInformation.Keywords = result.WebPageInformation.Keywords;

            doc.DocumentInformation.Author = "Pdf";
            doc.DocumentInformation.CreationDate = DateTime.Now;
            // create a new pdf font (component standard font)
            PdfFont font1 = doc.AddFont(PdfStandardFont.TimesRoman);
            font1.Size = 30;
            doc.Save(stream);
            doc.Close();
            return File(stream.ToArray(), "application/pdf");
        }
        public ActionResult GeneratePdfOrPrint(string date)
        {
            var model = new HomeModel();
            model.Date = date;
            model.Date = (model.Date == string.Empty || model.Date == null) ? DateTime.Now.DateFormat() : model.Date;
            model.LoadData();
            return View(model);
        }
    }
}
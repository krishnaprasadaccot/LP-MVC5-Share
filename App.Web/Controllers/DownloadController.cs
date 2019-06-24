using App.Entities;
using App.Web.Models;
using AutoMapper;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class DownloadController : BaseController
    {
        // GET: Download
        public ActionResult Index(int appId)
        {
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url 
            PdfDocument doc = converter.ConvertUrl(Request.Url.AbsoluteUri.ToString().ToLower().Replace("index", $"Receipt/{appId}"));

            // save pdf document 
            byte[] pdf = doc.Save();

            // close pdf document 
            doc.Close();

            // return resulted pdf document 
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = $"Receipt-{DateTime.Now.Ticks}.pdf";
            return fileResult;
        }


        public ActionResult Receipt(int id)
        {
            var savedApp = ApiGet<Application>(CONSTANTS.ApiUrls.BASE_ADDRESS, string.Format(CONSTANTS.ApiUrls.APPLICATION_GET, id));
            return View(Mapper.Map<Application, ApplicationViewModel>(savedApp));
        }

    }
}
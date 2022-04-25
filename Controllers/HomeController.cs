using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Task2.Models;
using MVC_Task2.Models.VMs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult UploadFiles()
        {
            FileVM vm = new FileVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadFiles(IFormFile[] Files)
        {
            List<string> _errors = new List<string>();
            ViewBag.ErrorMSG = "";
            if (Files.Count() > 3)
            {
                _errors.Add("Files Exceeded than 3");
            }
            foreach (var item in Files)
            {
                if (!(item.FileName.EndsWith(".pdf") || item.FileName.EndsWith(".docx")))
                {

                    _errors.Add(item.FileName + " contains invalid File Extension");
                }
                if (item.Length / (1024) > (2 * 1024))
                {
                    _errors.Add(item.FileName + " size exceeded from 2MB");
                }
            }
            ViewBag.Errors = _errors.ToArray();
            if (_errors.Count() == 0)
                return RedirectToAction("Index");
            return View();
        }




    }
}

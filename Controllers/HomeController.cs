using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Task2.Models;
using MVC_Task2.Models.VMs;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IHostingEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
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

        public IActionResult Success()
        {
            return View();
        }

        
        public IActionResult UploadFiles()
        {
            FileVM vm = new FileVM();
            return View(vm);
        }
        //Using Check to inforce data integrity
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
            if(Files.Count() < 3)
            {
                _errors.Add("Files are less than 3");
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
            if (_errors.Count() == 0 && ModelState.IsValid)
            {
                foreach(var item in Files)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "files");
                    string filePath = Path.Combine(uploadsFolder, item.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                }
                return RedirectToAction("Success");
            }
            return View();
        }


        public IActionResult UploadFiles2()
        {
            FileVM2 vm = new FileVM2();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadFiles2(FileVM2 vm)
        {
            if(ModelState.IsValid)
            {
                foreach (var item in vm.Files)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "files2");
                    string filePath = Path.Combine(uploadsFolder, item.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                }
                return RedirectToAction("Success");
            }
            return View();
        }

        public IActionResult UploadFiles3()
        {
            FileVM3 fileVM3 = new FileVM3();
            return View(fileVM3);
        }

        [HttpPost]
        [Multipart]
        [AutoValidateAntiforgeryToken]
        public IActionResult UploadFiles3(FileVM3 fileVM3)
        {
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "files3");
                string filePath = Path.Combine(uploadsFolder, fileVM3.File1.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileVM3.File1.CopyTo(fileStream);
                }

                uploadsFolder = Path.Combine(_environment.WebRootPath, "files3");
                filePath = Path.Combine(uploadsFolder, fileVM3.File2.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileVM3.File2.CopyTo(fileStream);
                }

                uploadsFolder = Path.Combine(_environment.WebRootPath, "files3");
                filePath = Path.Combine(uploadsFolder, fileVM3.File3.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileVM3.File3.CopyTo(fileStream);
                }
                return RedirectToAction("Success");
            }
            return View();
        }
    }
}

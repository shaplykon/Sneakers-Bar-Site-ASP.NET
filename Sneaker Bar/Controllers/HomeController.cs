using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;
using System;
using System.IO;

namespace Sneaker_Bar.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SneakersRepository sneakersRepository;

        public HomeController(SneakersRepository _sneakersRepository, IWebHostEnvironment _webHostEnvironment)
        {
            sneakersRepository = _sneakersRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        public ActionResult Index()
        {
            var model = sneakersRepository.GetSneakers();
            return View(model);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult SneakersEdit(int id)
        {
            Sneakers sneakers = id == 0 ?
                new Sneakers() :
                sneakersRepository.GetSneakersById(id);            
            return View(sneakers);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpPost]
        public IActionResult SneakersEdit(SneakersViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(viewModel);

                Sneakers sneakers = new Sneakers
                {
                    Id = viewModel.Id,
                    Company = viewModel.Company,
                    Model = viewModel.Model,
                    Price = viewModel.Price,
                    ReleaseDate = viewModel.ReleaseDate,
                    ImageData = uniqueFileName,
                };
                sneakersRepository.SaveSneakers(sneakers);
                return RedirectToAction(nameof(Index));
            }


            return View((Sneakers)viewModel);
        }

        private string UploadedFile(SneakersViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageData != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageData.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageData.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpPost]
        public IActionResult SneakersDelete(int Id)
        {
            sneakersRepository.DeleteSneakers(new Sneakers { Id = Id });
            return RedirectToAction("Index");
        }
    }
}

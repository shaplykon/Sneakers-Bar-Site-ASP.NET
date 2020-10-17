using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;
using System.IO;

namespace Sneaker_Bar.Controllers
{
    public class HomeController : Controller
    {

        private readonly SneakersRepository sneakersRepository;

        public HomeController(SneakersRepository _sneakersRepository) {
            sneakersRepository = _sneakersRepository;
        }

        public ActionResult Index() {
            var model = sneakersRepository.GetSneakers();    
            return View(model);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult SneakersEdit(int id) {
            Sneakers sneakers = id == 0 ? 
                new Sneakers() : 
                sneakersRepository.GetSneakersById(id);
            return View(sneakers);        
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpPost]
        public IActionResult SneakersEdit(Sneakers sneakers, IFormFile image) {
            if (image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }
                sneakers.ImageData = imageData;
            }


          //  if (ModelState.IsValid)
           // {
                
               sneakersRepository.SaveSneakers(sneakers);


                return RedirectToAction("Index");
          //  }
            return View(sneakers);
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

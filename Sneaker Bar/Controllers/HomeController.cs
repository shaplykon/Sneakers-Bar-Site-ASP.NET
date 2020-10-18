using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;

namespace Sneaker_Bar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SneakersRepository sneakersRepository;

        public HomeController(SneakersRepository _sneakersRepository)
        {
            sneakersRepository = _sneakersRepository;
        }

        public ActionResult Index()
        {
            var model = sneakersRepository.GetSneakers();
            return View(model);
        }
    }
}

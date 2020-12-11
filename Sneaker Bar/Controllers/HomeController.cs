using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sneaker_Bar.Hubs;
using Sneaker_Bar.Models;

namespace Sneaker_Bar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SneakersRepository sneakersRepository;
        private readonly ArticleRepository articleRepository;

        public HomeController(
            SneakersRepository _sneakersRepository, 
            ArticleRepository _articleRepository)
        {
            articleRepository = _articleRepository;
            sneakersRepository = _sneakersRepository;
        }

        public ActionResult IndexAsync()
        {
            var Sneakers = sneakersRepository.GetSneakers();
            var Articles = articleRepository.getLatestArticles();
            ViewBag.Sneakers = Sneakers;
            ViewBag.Articles = Articles;
            return View();
        }
    }
}

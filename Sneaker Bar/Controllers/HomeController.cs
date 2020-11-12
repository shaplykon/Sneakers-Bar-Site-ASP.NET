using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;

namespace Sneaker_Bar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SneakersRepository sneakersRepository;
        private readonly ArticleRepository articleRepository;

        public HomeController(SneakersRepository _sneakersRepository, ArticleRepository _articleRepository)
        {
            articleRepository = _articleRepository;
            sneakersRepository = _sneakersRepository;
        }

        public ActionResult Index()
        {
            var Sneakers = sneakersRepository.GetSneakers();
            var Articles = articleRepository.getLatestArticles();
      
            ViewBag.Sneakers = Sneakers;
            ViewBag.Articles = Articles;
            return View();
        }
    }
}

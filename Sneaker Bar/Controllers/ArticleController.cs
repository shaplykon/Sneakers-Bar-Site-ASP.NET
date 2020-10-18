using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Model;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Sneaker_Bar.Controllers
{
    public class ArticleController : Controller
    {
        ArticleRepository articleRepository;
        CommentRepository commentRepository;
        private IWebHostEnvironment webHostEnvironment;

        public ArticleController(
            ArticleRepository _articleRepository,
            IWebHostEnvironment _webHostEnvironment,
            CommentRepository _commentRepository)
        {
            commentRepository = _commentRepository;
            articleRepository = _articleRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet]
        public IActionResult ArticleEdit(int id)
        {
            Article article = id == default ? new Article() : articleRepository.getArticleById(id);
            return View(article);
        }

        [HttpPost]
        public IActionResult ArticleEdit(ArticleViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(viewModel);

                Article article = new Article
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Text = viewModel.Text,
                    UserId = int.Parse(HttpContext.User.Identity.Name),
                    Date = DateTime.Now,
                    ImageData = uniqueFileName,
                };
                articleRepository.SaveArticle(article);
                return RedirectToAction("Index");
            }
            return View((Article)viewModel);
        }
        private string UploadedFile(ArticleViewModel viewModel)
        {
            string uniqueFileName = null;

            if (viewModel.ImageData != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/articles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.ImageData.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.ImageData.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult ArticleDetail(int Id)
        {
            Article article = articleRepository.getArticleById(Id);
            ViewBag.Сomments = commentRepository.getCommentsByArticleId(article.Id);
            ViewBag.Article = article;

            return View();
        }


    }
}

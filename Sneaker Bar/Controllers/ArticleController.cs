using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Model;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;
using System;
using System.Collections.Generic;
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
        private readonly UserManager<IdentityUser> userManager;

        public ArticleController(
            ArticleRepository _articleRepository,
            IWebHostEnvironment _webHostEnvironment,
            CommentRepository _commentRepository,
            UserManager<IdentityUser> _userManager)
        {
            commentRepository = _commentRepository;
            articleRepository = _articleRepository;
            webHostEnvironment = _webHostEnvironment;
            userManager = _userManager;
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
                    UserId = Guid.Parse(userManager.GetUserId(HttpContext.User)),
                    Date = DateTime.Now,
                    ImageData = uniqueFileName,
                    AuthorName = HttpContext.User.Identity.Name
                };
                articleRepository.SaveArticle(article);
                return RedirectToAction("Index", "Home");
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
            article.Views++;
            articleRepository.SaveArticle(article);
            List<Comment> comments = commentRepository.getCommentsByArticleId(article.Id).ToList();
            ViewBag.Article = article;
            ViewBag.Comments = comments;
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult CommentAdd(Comment comment, int articleId)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
                comment.Date = DateTime.Now;
                comment.UserId = userId;
                comment.AuthorName = HttpContext.User.Identity.Name;
                commentRepository.SaveComment(comment);
                Article article = articleRepository.getArticleById(articleId);
                article.CommentsAmount++;
                articleRepository.SaveArticle(article);
            }
            return RedirectToAction("ArticleDetail", "Article", new { Id = articleId });
        }
        [HttpPost]
        public IActionResult DeleteComment(int commentId, int articleId)
        {
            Article article = articleRepository.getArticleById(articleId);
            article.CommentsAmount--;
            articleRepository.SaveArticle(article);
            commentRepository.DeleteCommentById(commentId);
            return RedirectToAction("ArticleDetail", "Article", new { Id = articleId });
        }

        [HttpGet]
        public IActionResult ArticleIndex() {
            List<Article> articles = articleRepository.getArticles();
            ViewBag.Articles = articles;
            return View();
        }

        [HttpPost]
        public IActionResult ArticleDelete(int articleId) {
            articleRepository.DeleteArticle(articleRepository.getArticleById(articleId));
            return RedirectToAction("Index", "Home");
        }
    }
}

﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;
using System;
using System.IO;
using System.Net.Http;

namespace Sneaker_Bar.Controllers
{
    public class ArticleController : Controller
    {
        ArticleRepository articleRepository;
        private IWebHostEnvironment webHostEnvironment;

        public ArticleController(ArticleRepository _articleRepository, IWebHostEnvironment  _webHostEnvironment) {
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
        public IActionResult ArticleEdit(ArticleViewModel viewModel) {

            if (ModelState.IsValid) {
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
        private string UploadedFile(ArticleViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageData != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/articles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageData.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageData.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
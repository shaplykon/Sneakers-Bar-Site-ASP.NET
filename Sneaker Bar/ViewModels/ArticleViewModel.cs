using Microsoft.AspNetCore.Http;
using Sneaker_Bar.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Sneaker_Bar.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Input Tile")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Input article body")]
        public string Text { get; set; }


        [Required(ErrorMessage = "Select Image")]
        public IFormFile ImageData { get; set; }


        public static explicit operator Article(ArticleViewModel viewModel)
        {
            Article article = new Article();
            if (viewModel.Id != default) {
                article.Id = viewModel.Id;
            }
            if (viewModel.Title != default)
            {
                article.Title = viewModel.Title;
            }
            if (viewModel.Text != default)
            {
                article.Text = viewModel.Text;
            }
            if (viewModel.ImageData != null) {
               article.ImageData = viewModel.ImageData.FileName;
            }

            return article;
        }

    }

}

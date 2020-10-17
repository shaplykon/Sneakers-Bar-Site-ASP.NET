using Microsoft.AspNetCore.Http;
using Sneaker_Bar.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Sneaker_Bar.ViewModels
{
    public class SneakersViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please input price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please input model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please input company")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please choose date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Sneakers Picture")]
        public IFormFile ImageData { get; set; }

        public static explicit operator Sneakers(SneakersViewModel viewModel)
        {
            Sneakers sneakers = new Sneakers();
            if (viewModel.Id != default) {
                sneakers.Id = viewModel.Id;
            }
            if (viewModel.Company != default)
            {
                sneakers.Company = viewModel.Company;
            }
            if (viewModel.Model != default)
            {
                sneakers.Model = viewModel.Model;
            }
            if (viewModel.Price != default)
            {
                sneakers.Price = viewModel.Price;
            }
            if (viewModel.ReleaseDate != default)
            {
                sneakers.ReleaseDate= viewModel.ReleaseDate;
            }
            if (viewModel.ImageData != null) {
                sneakers.ImageData = viewModel.ImageData.FileName;
            }

            return sneakers;
        }

    }

}

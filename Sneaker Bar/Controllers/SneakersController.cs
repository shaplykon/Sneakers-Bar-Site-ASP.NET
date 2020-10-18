 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;

namespace Sneaker_Bar.Controllers
{
    public class SneakersController : Controller
    {
        SneakersRepository sneakersRepository;
        PurchaseRepository purchaseRepository;
        CommentRepository commentRepository;
        IWebHostEnvironment webHostEnvironment;

        public SneakersController(
            SneakersRepository _sneakersRepository, PurchaseRepository _purchaseRepository, 
            CommentRepository _commentRepository, IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            commentRepository = _commentRepository;
            sneakersRepository = _sneakersRepository;
            purchaseRepository = _purchaseRepository;
        }

        [HttpGet]
        public IActionResult SneakersDetail(int Id)
        {
            Sneakers sneakers = sneakersRepository.GetSneakersById(Id);

           /* if (sneakers.commentIds.Count() > 0)
            {
                List<Comment> comments = commentRepository.getCommentsByIdArray(sneakers.commentIds);
                ViewBag.comments = comments;
            }*/

            ViewBag.sneakers = sneakers;
 
            if (User.Identity.IsAuthenticated)
            {
                if (purchaseRepository.IsInPurchases(int.Parse(User.Identity.Name), Id))
                {
                    ViewBag.isInCart = true;
                }

                else
                {
                    ViewBag.isInCart = false;
                }
            }
            else
            {
                ViewBag.isInCart = false;
            }

            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult AddToShoppingCart(int sneakersId)
        {
            int userId = int.Parse(User.Identity.Name);
            if (purchaseRepository.IsInPurchases(int.Parse(User.Identity.Name), sneakersId))
            {
                purchaseRepository.DeletePurchaseById(int.Parse(User.Identity.Name), sneakersId);
            }

            else
            {
                purchaseRepository.SavePurchase(
                new Purchase
                {
                    PurchaseId = 0,
                    UserId = userId,
                    SneakersId = sneakersId,
                    Date = DateTime.Now,

                });
            }
            return RedirectToAction("SneakersDetail", new { Id = sneakersId });
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            int userId = int.Parse(User.Identity.Name);
            List<Purchase> purchases = purchaseRepository.GetPurchaseByUserId(userId).ToList();
            List<Sneakers> sneakers = new List<Sneakers>();
            foreach (Purchase purchase in purchases)
            {
                sneakers.Add(sneakersRepository.GetSneakersById(purchase.SneakersId));
            }
            ViewBag.sneakers = sneakers;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteFromShoppingCart(int Id)
        {
            purchaseRepository.DeletePurchaseById(int.Parse(User.Identity.Name), Id);

            return Redirect("/Sneakers/ShoppingCart");
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
                return RedirectToAction(nameof(HomeController.Index));
            }


            return View((Sneakers)viewModel);
        }

        private string UploadedFile(SneakersViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageData != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/sneakers");
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

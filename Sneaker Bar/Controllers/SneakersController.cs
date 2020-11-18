 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Model;
using Sneaker_Bar.Models;
using Sneaker_Bar.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Logging;

namespace Sneaker_Bar.Controllers
{
    public class SneakersController : Controller
    {
        SneakersRepository sneakersRepository;
        PurchaseRepository purchaseRepository;
        CommentRepository commentRepository;
        IWebHostEnvironment webHostEnvironment;
        UserManager<IdentityUser> userManager;
        ILogger<SneakersController> logger;

        public SneakersController(
            SneakersRepository _sneakersRepository, PurchaseRepository _purchaseRepository,
            CommentRepository _commentRepository, IWebHostEnvironment _webHostEnvironment,
                    UserManager<IdentityUser> _userManager, ILogger<SneakersController> _logger)
        {
            logger = _logger;
            userManager = _userManager;
            webHostEnvironment = _webHostEnvironment;
            commentRepository = _commentRepository;
            sneakersRepository = _sneakersRepository;
            purchaseRepository = _purchaseRepository;
        }

        [HttpGet]
        public IActionResult SneakersDetail(int Id)
        {
            Sneakers sneakers;
            try
            {
                sneakers = sneakersRepository.GetSneakersById(Id);
            }
            catch (InvalidOperationException)
            {
                logger.LogError("Error occured while trying to get sneakers with Id: {0}", Id);
                ViewBag.title = "Requsted sneakers were not found";
                ViewBag.message = "It is probably a mistake in your request";
                return View("Error");
            }
            ViewBag.sneakers = sneakers;

            if (User.Identity.IsAuthenticated)
            {
                if (purchaseRepository.IsInPurchases(Guid.Parse(userManager.GetUserId(HttpContext.User)), Id))
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
            Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
            if (purchaseRepository.IsInPurchases(userId, sneakersId))
            {
                purchaseRepository.DeletePurchaseById(userId, sneakersId);
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
            return RedirectToAction("SneakersDetail", "Sneakers", new { Id = sneakersId });
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
            List<Purchase> purchases = purchaseRepository.GetPurchaseByUserId(userId).ToList();
            List<Sneakers> sneakers = new List<Sneakers>();
            double totalPrice = 0;
            foreach (Purchase purchase in purchases)
            {
                Sneakers sneaker = sneakersRepository.GetSneakersById(purchase.SneakersId);
                totalPrice += sneaker.Price;
                sneakers.Add(sneaker);
            }
            ViewBag.totalPrice = totalPrice;
            ViewBag.sneakers = sneakers;

            return View();
        }

   
        public IActionResult DeleteFromShoppingCart(int sneakersId)
        {
            purchaseRepository.DeletePurchaseById(Guid.Parse(userManager.GetUserId(HttpContext.User)), sneakersId);

            return RedirectToAction("ShoppingCart", "Sneakers");
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
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> ConfirmOrderAsync(double totalPrice) {
            String email = User.Identity.Name;

            List<Sneakers> orderList = new List<Sneakers>();
            List<Purchase> purchases = purchaseRepository.GetPurchaseByUserId(Guid.Parse(userManager.GetUserId(HttpContext.User)));
            foreach (Purchase purchase in purchases) {
                orderList.Add(sneakersRepository.GetSneakersById(purchase.SneakersId));
            }
        
            MailService mailService = new MailService();
            await mailService.SendEmailAsync(email, "Order confirmation message", BuildPreConfirmationMessage(email, totalPrice, orderList));
            return RedirectToAction("OrderConfirmation");
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> OrderConfirmationAsync(Guid Id)
        {
            ViewBag.PurchaseId = Id.ToString();
            String email = User.Identity.Name;
            if (Id != Guid.Empty) {
               List<Purchase> purchases =  purchaseRepository.GetPurchaseByUserId(Guid.Parse(userManager.GetUserId(HttpContext.User)));
                foreach (Purchase purchase in purchases) {
                    purchaseRepository.DeletePurchase(purchase);
                }
                MailService mailService = new MailService();
                await mailService.SendEmailAsync(email, "Order confirmation message", BuildConfirmationMessage(email));
            }
            return View();
        }


        private string BuildConfirmationMessage(string email) {

            string message = "<h1> <p>" + email + ", our order was successfully confrirmed!</p>\n";
            message += "<p>Order date: " + DateTime.Now.ToString();
            return message;
        }


        private string BuildPreConfirmationMessage(string email, double totalPrice, List<Sneakers> orderList)
        {

            string message = "<h1> <p>" + email + ", check your order information!</p>\n"
                + " <p>Total price is " + totalPrice.ToString() + "$ (" + orderList.Count + " items)" + "</p>";
            message += "<ol>";
            foreach (Sneakers sneakers in orderList)
            {
                message += "<li>";
                message += sneakers.Company + " ";
                message += sneakers.Model;
                message += "</li>";
            }
            message += "</ol>";

            message += "<p>Order date: " + DateTime.Now.ToString();
   
            message  += "<p><a href = \"" + Request.Scheme + "://" + Request.Host.Value + "/Sneakers/OrderConfirmation/" + Guid.Parse(userManager.GetUserId(HttpContext.User)) +
                "\">Click this link to confirm your order!</ a >";
           
            return message;
        }
    }
}

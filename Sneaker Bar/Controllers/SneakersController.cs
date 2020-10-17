using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Bar.Models;


namespace Sneaker_Bar.Controllers
{
    public class SneakersController : Controller
    {
        SneakersRepository sneakersRepository;
        PurchaseRepository purchaseRepository;
        CommentRepository commentRepository;

        public SneakersController(
            SneakersRepository _sneakersRepository,
            PurchaseRepository _purchaseRepository,
            CommentRepository _commentRepository)
        {
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
                    //isConfirmed = false,
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
    }
}

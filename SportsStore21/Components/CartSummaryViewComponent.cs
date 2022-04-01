using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore21.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        public Cart cart { get; set; }

        public CartSummaryViewComponent(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}

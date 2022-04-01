using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore21.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        [HttpGet]
        [Authorize]
        public ViewResult List() =>
            View( repository.Orders.Where( o => o.Shipped == false ) );

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders
                            .FirstOrDefault( o => o.OrderID == orderID );

            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
            if (cart.Lines.Count() == 0)            
                ModelState.AddModelError("", "Desculpe, seu carrinho está vazio!");

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else 
            {
                return View();
            }
        }
    }
}

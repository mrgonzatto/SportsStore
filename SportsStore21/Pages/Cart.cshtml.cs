using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore21.Models;

namespace SportsStore21.Pages
{
    public class CartModel : PageModel
    {
        public IStoreRepository repository;

        public CartModel(IStoreRepository repository, Cart cartService)
        {
            this.repository = repository;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";            
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);            
            Cart.AddItem(product, 1);            
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productID, string returnUrl)        
        {
            Cart.RemoveLine(
                Cart.Lines.First(cl => cl.Product.ProductID == productID ).Product
            );
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}

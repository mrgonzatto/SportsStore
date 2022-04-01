using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore21.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IStoreRepository repository;

        public AdminController(IStoreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(repository.Products);
        }

        [HttpGet]
        public ViewResult Edit( int productId ) 
        {
            return View( 
                repository.Products.FirstOrDefault( p => p.ProductID == productId ) 
            );
        }

        [HttpPost]
        public IActionResult Edit(Product product) 
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Produto {product.Name} salvo com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete( int productId ) 
        {
            Product deletedProduct =
                repository.DeleteProduct(productId);

            if ( deletedProduct != null )
            {
                TempData["message"] = 
                    $"Produto {deletedProduct.Name} excluído com sucesso!";
            }

            return RedirectToAction("Index");
        }
    }
}

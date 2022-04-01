using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models;
using System.Linq;
using SportsStore21.Models.ViewModels;

namespace SportsStore21.Controllers
{
    public class HomeController : Controller
    {

        private IStoreRepository repository;

        public int PageSize = 2;

        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public ViewResult Index(string category, int productPage = 1) 
        {
            ProductsListViewModel plvm =
                new ProductsListViewModel {
                    Products = repository.Products
                                    .Where(p => category == null  || p.Category == category)
                                    .OrderBy(p => p.ProductID)
                                    .Skip((productPage - 1) * PageSize)
                                    .Take(PageSize),
                    PagingInfo = new PagingInfo { 
                        CurrentCategory = category,
                        CurrentPage = productPage,                        
                        ItemsPerPage = PageSize,
                        TotalItems = ( category == null ? 
                                        repository.Products.Count() : 
                                        repository.Products
                                            .Where( p => p.Category == category )
                                            .Count()
                                     )
                    }
                };

            return View(plvm);
        }

        /*
        public IActionResult Index(int productPage = 1)
        {
            return View( repository.Products.OrderBy( p => p.ProductID )
                                    .Skip( (productPage - 1) * PageSize) 
                                    .Take(PageSize)
            );
        }
        */
    }
}

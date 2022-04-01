using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models;
using System.Linq;

namespace SportsStore21.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {            
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View( 
                repository.Products
                .Select( prod => prod.Category )
                .Distinct()
                .OrderBy( categoria => categoria )
            );
        }
    }
}

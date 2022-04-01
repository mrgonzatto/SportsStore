using System.Collections.Generic;

namespace SportsStore21.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product newProduct);
    }
}

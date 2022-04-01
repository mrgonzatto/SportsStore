using System.Linq;

namespace SportsStore21.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}

using System.Linq;

namespace SportsStore21.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        // Propriedade privada para acesso do repositório ao contexto de dados
        // do SQLServer
        private StoreDbContext context;

        // Método construtor com injeção de dependência do contexto de dados
        // que foi previamente registrado como serviço no Startup.cs
        public EFStoreRepository(StoreDbContext ctx) 
        {
            this.context = ctx;
        }

        public IQueryable<Product> Products => context.Products;

        public void SaveProduct( Product product )
        {
            // Se o código do produto for zero, é um novo produto
            // senão é um produto sendo editado
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry =
                    context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }       

        public Product DeleteProduct( int productID )
        {
            Product dbEntry =
                context.Products
                .FirstOrDefault(p => p.ProductID == productID);            

            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}

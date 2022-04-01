using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore21.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) 
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Products.Any()) {

                context.Products.AddRange(

                    new Product 
                    { 
                        Name = "Perfume", Description = "O Boticário", 
                        Category = "Cosmético", Price = 100
                    },
                    new Product
                    {
                        Name = "Bola de capotão", Description = "Bola dente de leite",
                        Category = "Futebol", Price = 40
                    },
                    new Product
                    {
                        Name = "Bola de salão",
                        Description = "Bola Penalty",
                        Category = "Futebol",
                        Price = 50
                    },
                    new Product
                    {
                        Name = "Luva de goleiro",
                        Description = "Luva Adidas",
                        Category = "Futebol",
                        Price = 70
                    },
                    new Product
                    {
                        Name = "Meião",
                        Description = "Meia Dal Ponte",
                        Category = "Futebol",
                        Price = 25
                    }
                );

                context.SaveChanges();

            }
        }
    }
}

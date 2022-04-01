using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore21.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SportsStore21
{
    public class Startup
    {

        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IStoreRepository, EFStoreRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();

            services.AddRazorPages();
            // Ativa serviço de cache e sessão
            services.AddDistributedMemoryCache();
            services.AddSession();

            //AddScoped faz com que o mesmo objeto seja utilizado para satisfazer as 
            //requisições que solicitem instâncias de Cart
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

            //AddSingleton especifica que o mesmo objeto sempre será utilizado. O serviço 
            // criado diz ao ASP.NET Core para usar o Acesso ao Contexto do HTTP 
            // (HttpContextAccessor) este serviço é requerido para que possamos 
            // acessar a sessão atual do usuário na memória do servidor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlServer(Configuration["ConnectionStrings:LojaEsportesConnection"]);
            });

            services.AddDbContext<AppIdentityDbContext>(opts => {
                opts.UseSqlServer(Configuration["ConnectionStrings:LojaEsportesIdentity"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            // Especifica uso das páginas de erro 403/404/etc doi http
            app.UseStatusCodePages();
            // Define que a aplicação utilizará os arquivos estáticos 
            // css, js, img etc que ficam armazenados no dir www
            app.UseStaticFiles();            
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catpage",
                    "{category}/Page{productPage:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("page",
                    "Page{productPage}",
                    new { Controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("category", "{category}",
                    new { Controller = "Home", action = "Index", productPage = 1 });


                // Rota criada para especificar a variável de paginação da lista
                // de produtos
                endpoints.MapControllerRoute( "pagination", 
                    "Products/Page{productPage}", 
                    new { Controller = "Home", Action = "Index", productPage =1 } );

                // Configura estrutura de rota padrão
                // website/controller/method/param
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);

        }
    }
}

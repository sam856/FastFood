using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Online_Fast_food_Delievery.Models;

namespace Online_Fast_food_Delievery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", options =>
                {

                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

            }
        );
           
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
         

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
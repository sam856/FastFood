using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Fast_food_Delievery.Models;
using System.Diagnostics;

namespace Online_Fast_food_Delievery.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var items = 
                context.Item.Include(x => x.Category)
                .Include(y => y.SubCategory).ToList();
            
            
            return View(items);
        }

        

       
    }
}
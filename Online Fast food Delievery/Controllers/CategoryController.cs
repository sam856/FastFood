using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Fast_food_Delievery.Models.Dto;
using System.Data;

namespace Online_Fast_food_Delievery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var ListCategory =
                appDbContext.Category.ToList().Select(
                    x => new CategoryDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                    }).ToList();
            return View(ListCategory);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryDto model )
        {
            Category category = new Category();
            category.Title = model.Title;
            appDbContext.Category.Add(category);
            appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Category = appDbContext.Category.Where(x => x.Id == id)
                .Select(x => new CategoryDto()
                {
                    Id=x.Id,
                    Title = x.Title,
                }
                ).FirstOrDefault();
            return View(Category);
        }
        [HttpPost]
        public IActionResult Edit(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                Category category = appDbContext.Category
                    .FirstOrDefault(x => x.Id == model.Id);
                if (category != null)
                {
                    category.Title = model.Title;
                    appDbContext.Category.Update(category);
                    appDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id )
        {
            if (ModelState.IsValid)
            {
                Category category = appDbContext.Category
                    .FirstOrDefault(x => x.Id == id);
                if (category != null)
                {
                    appDbContext.Category.Remove(category);
                    appDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}

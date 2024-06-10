using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Fast_food_Delievery.Models.Dto;
using System.Data;

namespace Online_Fast_food_Delievery.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ItemController : Controller
    {
        private readonly AppDbContext context;
        public ItemController(AppDbContext context)
        {
            this.context=context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = context.Item.Include(x => x.Category).Include(y => y.SubCategory).ToList();
            return View(items);
        }
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(context.Category, "Id", "Title");
            return View();

        }

        [HttpGet]
        public JsonResult GetSubCategories(int categoryId)
        {
            var subCategories = context.SubCategories
                                        .Where(sc => sc.CategoryId == categoryId)
                                        .Select(sc => new { sc.Id, sc.Title })
                                        .ToList();
            return Json(subCategories);
        }
        [HttpPost]
            public async Task <IActionResult>Create(ItemDto item )
            {
                if (ModelState.IsValid)
            {
                var Model = new Item();
               Model.Price = item.Price;
                Model.Title=item.Title;
                Model.Description=item.Description;
                Model.CategoryId=item.CategoryId;
                Model.Description = item.Description;
                Model.SubCategoryId=item.SubCategoryId;
                if (item.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await item.Image.CopyToAsync(memoryStream);
                        Model.Image = memoryStream.ToArray();
                    }
                }

                context.Item.Add(Model);
                context.SaveChanges();
                


            }
                return RedirectToAction("Index");
            }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Item = context.Item.Where(x => x.Id == id).FirstOrDefault();
            if (Item != null) {
                context.Item.Remove(Item);
                context.SaveChanges();
            }
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var Item = context.Item
                        .Where(y => y.Id == id)
                        .Include(x => x.Category)
                        .Include(y => y.SubCategory)
                        .FirstOrDefault();
            var Vm = new ItemDto();
            Vm.Title = Item.Title;
            Vm.Description=Item.Description;
            Vm.SubCategoryId = Item.SubCategoryId;
            Vm.CategoryId = Item.CategoryId;
            Vm.Price=Item.Price;

            ViewBag.Category = new SelectList(context.Category, "Id", "Title",Vm.CategoryId);
            ViewBag.SubCategory = new SelectList(context.SubCategories.Where(x=>x.CategoryId==Vm.CategoryId), "Id", "Title", Vm.SubCategoryId);

            return View(Vm);

        }

        [HttpPost]
        public  async Task <IActionResult> Edit(ItemDto item)
        {
            var Model = context.Item.Where(x => x.Id == item.Id).FirstOrDefault();
            if (ModelState.IsValid) {
                Model.Price = item.Price;
                Model.Title = item.Title;
                Model.Description = item.Description;
                Model.CategoryId = item.CategoryId;
                Model.Description = item.Description;
                Model.SubCategoryId = item.SubCategoryId;
                if (item.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await item.Image.CopyToAsync(memoryStream);
                        Model.Image = memoryStream.ToArray();
                    }
                }

                context.Item.Update(Model);
                context.SaveChanges();

            }
            return RedirectToAction("Index");

        }

       
    }
}

 

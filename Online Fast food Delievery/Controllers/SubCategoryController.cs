using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Fast_food_Delievery.Models.Dto;
using Online_Fast_food_Delievery.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Online_Fast_food_Delievery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        
        private readonly AppDbContext _context;
        public SubCategoryController(AppDbContext _context)
        {
            this._context = _context;
            
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allSubCategories = _context.SubCategories.Include(x => x.Category).ToList();

            return View(allSubCategories);
        }
        [HttpGet]

        public IActionResult Create()
        {
            var SumModel = new SubCategoryDto();
            ViewBag.Category = new SelectList(_context.Category, "Id", "Title");
            return View(SumModel);
        }
        [HttpPost]
        public IActionResult Create(SubCategoryDto subCategoryDto)
        {
            var SumModel = new SubCategory();
            SumModel.CategoryId = subCategoryDto.CategoryId;
            SumModel.Title=subCategoryDto.Title;    
            _context.SubCategories.Add(SumModel);
            _context.SaveChanges();
            return  RedirectToAction("Index");  
        }


        [HttpGet]

        public IActionResult Edit (int id )
        {
            var SumModel = new SubCategoryDto();
            var SubCategory = _context.SubCategories.Where(x => x.Id == id).Include(x=>x.Category).FirstOrDefault();
            if (SubCategory != null)
            {

                SumModel.Title = SubCategory.Title;
                SumModel.CategoryId= SubCategory.CategoryId;
                ViewBag.Category = new SelectList(_context.Category, "Id", "Title", SubCategory.CategoryId);
            }
            return View(SumModel);
        }
        [HttpPost]
        public IActionResult Edit(SubCategoryDto SumModel)
        {
            if (ModelState != null)
            {
                var SubCategory = _context.SubCategories.Where(x => x.Id == SumModel.Id).FirstOrDefault();

                if (SubCategory != null)
                {
                    SubCategory.Title = SumModel.Title;
                    SubCategory.CategoryId=SumModel.CategoryId;
                    _context.SubCategories.Update(SubCategory);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int  id )
        {
            
                var SubCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();

                if (SubCategory != null)
                {
                _context.SubCategories.Remove(SubCategory);
                _context.SaveChanges();
                }
            
            return RedirectToAction("Index");
        }
    }
}

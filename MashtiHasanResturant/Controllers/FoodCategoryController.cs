using MashtiHasanRestaurant.Core.Common;
using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MashtiHasanResturant.Controllers
{
    public class FoodCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public FoodCategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }
        private async Task BindRoots()
        {
            var cats = await _categoryService.GetRoots();
            cats.Insert(0, new NewsCategoryListItem { CategoryName = "دسته اصلی" });
            ViewBag.drpRoots = new SelectList(cats, "NewsCategoryID", "CategoryName");
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {

            var cats = await _categoryService.GetAll();
            return View(cats);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var op = await _categoryService.Delete(id);
            return Json(op);
        }
        public async Task<IActionResult> Update(int id)
        {

            var cat = await _categoryService.Get(id);
            return View(cat);
        }
        [HttpPost]
        public async Task<JsonResult> Update(FoodCategoryAddEditModel cat)
        {
            var op = await _categoryService.Update(cat);
            return Json(op);
        }

        public async Task<IActionResult> Add()
        {

            await BindRoots();
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Add(FoodCategoryAddEditModel cat)
        {
            if (ModelState.IsValid)
            {
                
                var op = await _categoryService.Add(cat);
                return Json(op);
            }
            else
            {
                return Json(new OperationResult().ToFailed("error in sending data"));
            }

        }
    }
}

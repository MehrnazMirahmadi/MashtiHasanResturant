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

    }
}

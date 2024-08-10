using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MashtiHasanRestaurant.Core.Security;
using MashtiHasanRestaurant.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace MashtiHasanResturant.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly ResturantMashtiHasanContext _resturantMashtiContext;

        public FoodController(IFoodService foodService,ResturantMashtiHasanContext resturantMashtiHasan)
        {
            _foodService = foodService;
            _resturantMashtiContext = resturantMashtiHasan;
        }
        [Route("GetFoods")]
        public IActionResult Index()
        {
            var foods = _foodService.GetFoods();
            return View(foods);
        }

   #region AddFood
        [Route("AddFood")]
        public IActionResult AddFood()
        {
            var cats = _resturantMashtiContext.Category
                .Select(x => new CategoryListItem
                {
                    CategoryID = x.CategoryId,
                    CategoryName = x.CategoryName
                })
                .ToList();

            var vm = new AddFoodViewModel
            {
                Cat = new SelectList(cats, "CategoryID", "CategoryName")
            };

            return View(vm);
        }

        [HttpPost]
        [Route("AddFood")]
        public async Task<IActionResult> AddFood(Food food)
        {
            if (ModelState.IsValid)
            {
                _resturantMashtiContext.Food.Add(food);
                await _resturantMashtiContext.SaveChangesAsync();
                return RedirectToAction("GetFoods");
            }

            var cats = _resturantMashtiContext.Category
                .Select(x => new CategoryListItem
                {
                    CategoryID = x.CategoryId,
                    CategoryName = x.CategoryName
                })
                .ToList();

            var vm = new AddFoodViewModel
            {
                Cat = new SelectList(cats, "CategoryID", "CategoryName")
            };

            return View(vm);
        }

        #endregion
        #region DetailFood
        public IActionResult DetailFood(int id)
        {
                var food = _foodService.GetFoodById(id);
                if (food == null)
                {
                    return NotFound();
                }
                return View(food);
         
        }
        #endregion
        #region Delete 
        [HttpPost]
        public async Task<IActionResult> Delete(int FoodId)
        {
            var food = await _resturantMashtiContext.Food.FirstOrDefaultAsync(x => x.FoodId == FoodId);
            _resturantMashtiContext.Food.Remove(food);
            await _resturantMashtiContext.SaveChangesAsync();
            return RedirectToAction("index");
        }
        #endregion
    }

}

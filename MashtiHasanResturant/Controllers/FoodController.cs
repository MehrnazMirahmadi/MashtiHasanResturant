using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MashtiHasanRestaurant.Core.Security;
using MashtiHasanRestaurant.DataLayer.Context;

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
    }

}

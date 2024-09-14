using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MashtiHasanRestaurant.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using MashtiHasanRestaurant.Core.DTOs.Search;

namespace MashtiHasanResturant.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly ICategoryService _categoryService;
        private readonly ResturantMashtiHasanContext _resturantMashtiContext;

        public FoodController(IFoodService foodService,ResturantMashtiHasanContext resturantMashtiHasan, ICategoryService categoryService)
        {
            _foodService = foodService;
            _resturantMashtiContext = resturantMashtiHasan;
            _categoryService = categoryService;
        }
        public async Task InflateCategories()
        {
            var cats = await _categoryService.GetAll();
            cats.Insert(0, new MashtiHasanRestaurant.Core.DTOs.NewsCategoryListItem
            {
                CategoryId = -1,
                CategoryName = "...Please Select"
            
               
            });
            ViewBag.Categories = new SelectList(cats, "CategoryId", "CategoryName");

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
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName
                })
                .ToList();

            var vm = new AddFoodViewModel
            {
                Cat = new SelectList(cats,"CategoryId","CategoryName")
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
                return RedirectToAction("FoodIndex");
            }

            var cats = _resturantMashtiContext.Category
                .Select(x => new CategoryListItem
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName
                })
                .ToList();

            var vm = new AddFoodViewModel
            {
                Cat = new SelectList(cats, "CategoryId", "CategoryName")
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
            return RedirectToAction("FoodIndex");
        }
        #endregion

        #region FoodIndex
        public async Task<IActionResult> FoodIndex(SearchItems sm)
        {
            await InflateCategories();
            return View(sm);
        }
        public async Task<IActionResult> FoodListAction(SearchItems sm)
        {
            return ViewComponent("FoodsList", sm);
        }
        #endregion
    }

}

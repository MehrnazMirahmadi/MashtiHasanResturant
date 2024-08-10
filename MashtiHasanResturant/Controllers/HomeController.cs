using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Entities;
using MashtiHasanResturant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MashtiHasanResturant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodService _foodService;

        public HomeController(ILogger<HomeController> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewData["GroupName"] = name;
            var categories = _foodService.GetCategoriesForFood(id);
            if (categories == null || categories.Count == 0)
            {
                _logger.LogWarning($"No categories found for food ID: {id}");
                return NotFound();
            }
            return View(categories);
        }

        [Route("ShowGroup/{categoryId}")]
        public async Task<IActionResult> ShowGroup(int categoryId)
        {
            var foodItems = await _foodService.GetFoodItemsByCategoryAsync(categoryId);
            if (foodItems == null || !foodItems.Any())
            {
                _logger.LogWarning($"No food items found for category ID: {categoryId}");
                return NotFound();
            }
            return View(foodItems);
        }

        public async Task<IActionResult> Index()
        {
            var foodItems = await _foodService.GetMenuItemsAsync();
            return View(foodItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

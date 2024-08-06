using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanResturant.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MashtiHasanResturant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodService _foodService;

        public HomeController(ILogger<HomeController> logger,IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }
        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewData["GroupName"] = name;
            var result = _foodService.GetCategoriesForFood(id);
            return View(result);
        }
        [Route("ShowGroup/{categoryId}")]
        public async Task<IActionResult> ShowGroup(int categoryId)
        {
            var foodItems = await _foodService.GetFoodItemsByCategoryAsync(categoryId);
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

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

        #region AddFood
        [HttpGet]
        public IActionResult AddFood()
        {
            var model = new AddFoodViewModel
            {
                Categories = _foodService.GetAllCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFood(AddFoodViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _foodService.GetAllCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();
                return View(model);
            }

            var food = new Food
            {
                FoodName = model.FoodName,
                Ingredient = model.Ingredients,
                UnitPrice = model.UnitPrice,
                CategoryId = model.CategoryId
            };

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/menu");

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                food.ImageAddress = $"/assets/img/menu/{fileName}";
            }

            _foodService.AddFood(food, model.ImageFile);
            return RedirectToAction("Index");
        }


        #endregion

        [HttpGet]
        [Route("Food/Edit/{id}")]
        public IActionResult EditFood(int id)
        {
            var food = _foodService.GetFoodById(id);
            if (food == null)
            {
                _logger.LogWarning($"Food not found with ID: {id}");
                return NotFound();
            }

            var model = new FoodItemViewModel
            {
                FoodId = food.FoodId,
                FoodName = food.FoodName,
                Ingredients = food.Ingredient,
                UnitPrice = food.UnitPrice,
                CategoryId = food.CategoryId
            };

            return View(model);
        }

        [HttpPost]
        [Route("Food/Edit/{id}")]
        public IActionResult EditFood(int id, FoodItemViewModel model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for editing food.");
                return View(model);
            }

            var food = _foodService.GetFoodById(id);
            if (food == null)
            {
                _logger.LogWarning($"Food not found with ID: {id}");
                return NotFound();
            }

            food.FoodName = model.FoodName;
            food.Ingredient = model.Ingredients;
            food.UnitPrice = model.UnitPrice;
            food.CategoryId = model.CategoryId;

            if (imageFile != null)
            {
                // You might want to delete the old image file here
                string imagePath = Path.Combine("wwwroot/assets/img/menu", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                food.ImageAddress = "/assets/img/menu/" + imageFile.FileName;
            }

            _foodService.UpdateFoodById(id);
            _logger.LogInformation($"Food item updated with ID: {id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Food/Delete/{id}")]
        public IActionResult DeleteFood(int id)
        {
            try
            {
                _foodService.DeleteFoodById(id);
                _logger.LogInformation($"Food item deleted with ID: {id}");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Food item not found for deletion with ID: {id}");
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

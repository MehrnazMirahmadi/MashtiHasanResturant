using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Context;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;



namespace MashtiHasanRestaurant.Core.Services
{
    public class FoodService : IFoodService
    {
        private readonly ResturantMashtiHasanContext _resturantMashtiHasanContext;

        public FoodService(ResturantMashtiHasanContext resturantMashtiHasanContext)
        {
            _resturantMashtiHasanContext = resturantMashtiHasanContext;
        }

        public List<Category> GetCategoriesForFood(int foodId)
        {
            return _resturantMashtiHasanContext.Food
                .Where(f => f.FoodId == foodId)
                .Select(f => f.Category)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _resturantMashtiHasanContext.Category;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _resturantMashtiHasanContext.Category
                .Select(c => new ShowGroupViewModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    FoodCount = _resturantMashtiHasanContext.Food.Count(f => f.CategoryId == c.CategoryId)
                })
                .ToList();
        }

        public async Task<List<FoodItemViewModel>> GetMenuItemsAsync()
        {
            return await _resturantMashtiHasanContext.Food
                .Include(f => f.Category)
                .Select(f => new FoodItemViewModel
                {
                    FoodId = f.FoodId,
                    FoodName = f.FoodName,
                    Ingredients = f.Ingredient,
                    UnitPrice = f.UnitPrice,
                    ImageAddress = f.ImageAddress,
                    CategoryName = f.Category.CategoryName,
                    CategoryId = f.CategoryId

                })
                .ToListAsync();
        }

        public async Task<List<FoodItemViewModel>> GetFoodItemsByCategoryAsync(int categoryId)
        {
            return await _resturantMashtiHasanContext.Food
                .Include(f => f.Category)
                .Where(f => f.CategoryId == categoryId)
                .Select(f => new FoodItemViewModel
                {
                    FoodId = f.FoodId,
                    FoodName = f.FoodName,
                    Ingredients = f.Ingredient,
                    UnitPrice = f.UnitPrice,
                    ImageAddress = f.ImageAddress,
                    CategoryName = f.Category.CategoryName,
                    CategoryId = f.CategoryId
                })
                .ToListAsync();
        }

        public List<Food> GetFoods()
        {
            return _resturantMashtiHasanContext.Food.ToList();
        }
        public int AddFood(AddFoodViewModel food)
        {
            _resturantMashtiHasanContext.Add(food);
            _resturantMashtiHasanContext.SaveChanges();
            return food.Food.FoodId;    
        }


        public Food GetFoodById(int id)
        {
            
            return _resturantMashtiHasanContext.Food
                .Include(f => f.Category)
                .FirstOrDefault(f => f.FoodId == id);
        }


        //public void UpdateFoodById(int id)
        //{
        //    var food = _resturantMashtiHasanContext.Food.Find(id);
        //    if (food == null)
        //    {
        //        throw new KeyNotFoundException("Food not found.");
        //    }

        //    _resturantMashtiHasanContext.Food.Update(food);
        //    _resturantMashtiHasanContext.SaveChanges();
        //}

        //public void DeleteFoodById(int id)
        //{
        //    var food = _resturantMashtiHasanContext.Food.Find(id);
        //    if (food == null)
        //    {
        //        throw new KeyNotFoundException("Food not found.");
        //    }

        //    _resturantMashtiHasanContext.Food.Remove(food);
        //    _resturantMashtiHasanContext.SaveChanges();
        //}
    }
}

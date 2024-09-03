using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.DTOs.Search;
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

        public Orders GetOrdersByCustomerId(int userId)
        {
           return _resturantMashtiHasanContext.Orders.FirstOrDefault(o => o.CustomerId == userId);
        }

        public async Task<List<FoodListItem>> GetAll()
        {
            return await _resturantMashtiHasanContext.Food.Select(foods => new FoodListItem
            { FoodId = foods.FoodId,
              CategoryName=foods.Category.CategoryName,
              ImageAddress= foods.ImageAddress,
              UnitPrice=foods.UnitPrice,
              Ingredients = foods.Ingredient,
              FoodName=foods.FoodName

            }).ToListAsync();


        }

        public async Task<ListComplexModel> Search(SearchItems sm)
        {
            ListComplexModel result = new ListComplexModel();

            var q = from foods in _resturantMashtiHasanContext.Food
                    select foods;

            if (!string.IsNullOrEmpty(sm.Name))
            {
                q = q.Where(x => x.FoodName.StartsWith(sm.Name));
            }

          
            if (sm.CategoryId > 0)
            {
                q = q.Where(x => x.CategoryId == sm.CategoryId);
            }
            if (sm.UnitPriceFrom != null) { 
            q = q.Where(x=>x.UnitPrice >=  sm.UnitPriceFrom);
            }
            if(sm.UnitPriceTo !=null)
            {
                q=q.Where(x=>x.UnitPrice <= sm.UnitPriceTo);
            }
            result.RecordCount = await q.CountAsync();

            result.FoodsList = await q.Skip(sm.PageIndex * sm.PageSize)
                .Take(sm.PageSize)
                .Select(foods => new FoodListItem
                {
                    CategoryName = foods.Category.CategoryName,
                    CategoryId = foods.CategoryId,
                    FoodName = foods.FoodName,
                    FoodId = foods.FoodId,
                    ImageAddress = foods.ImageAddress,
                    UnitPrice = foods.UnitPrice,
                    Ingredients = foods.Ingredient
                }).ToListAsync();

            return result;
        }

    }
}

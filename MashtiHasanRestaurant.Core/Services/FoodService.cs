using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Context;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Services
{
    public class FoodService :IFoodService
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

        //public async Task<List<FoodItemViewModel>> GetMenuItemsAsync()
        //{
        //    return await _resturantMashtiHasanContext.Food
        //        .Include(f => f.Category)
        //        .Select(f => new FoodItemViewModel
        //        {
        //            FoodId = f.FoodId,
        //            FoodName = f.FoodName,
        //            Ingredients = f.Ingredient,
        //            UnitPrice = f.UnitPrice,
        //            ImageAddress = f.ImageAddress,
        //            CategoryName = f.Category.CategoryName
        //        })
        //        .ToListAsync();
        //}
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

    }
}

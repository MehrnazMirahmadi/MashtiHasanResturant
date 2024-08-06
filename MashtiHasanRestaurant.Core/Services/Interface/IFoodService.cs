using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Services.Interface
{
    public interface IFoodService
    {
        //Task<List<FoodItemViewModel>> GetMenuItemsAsync();
        List<Category> GetCategoriesForFood(int foodId);
        IEnumerable<ShowGroupViewModel> GetGroupForShow();
        IEnumerable<Category> GetAllCategories();
        Task<List<FoodItemViewModel>> GetMenuItemsAsync();
        Task<List<FoodItemViewModel>> GetFoodItemsByCategoryAsync(int categoryId);
    }
}

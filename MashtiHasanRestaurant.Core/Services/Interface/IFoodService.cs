using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.DTOs.Search;
using MashtiHasanRestaurant.DataLayer.Entities;


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
        List<Food> GetFoods();
        int AddFood(AddFoodViewModel food);

        Food GetFoodById(int id);
        Orders GetOrdersByCustomerId(int userId);
        /*******************************************************/
        Task<List<FoodListItem>> GetAll();
        Task<ListComplexModel> Search(SearchItems sm);
    }
}

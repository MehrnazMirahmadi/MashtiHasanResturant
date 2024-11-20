using MashtiHasanRestaurant.Core.DTOs.Search;
using MashtiHasanRestaurant.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MashtiHasanResturant.ViewComponents
{
    [ViewComponent(Name = "FoodsList")]
    public class FoodsListViewComponent: ViewComponent
    {
        private IFoodService _foodService;
        public FoodsListViewComponent(IFoodService foodService)
        {
                _foodService = foodService;
        }
        public async Task<IViewComponentResult> InvokeAsync(SearchItems sm)
        {
            var r = await _foodService.Search(sm);

            return View(r.FoodsList);
        }
    }
}

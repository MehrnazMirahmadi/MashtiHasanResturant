using MashtiHasanRestaurant.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private readonly IFoodService _foodService;
        public ProductGroupsComponent(IFoodService foodService)
        {
            _foodService = foodService;       
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Components/ProductGroupsComponent.cshtml", _foodService.GetGroupForShow());
        }
    }
}

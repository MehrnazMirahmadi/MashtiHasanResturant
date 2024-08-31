using MashtiHasanRestaurant.Core.Common;
using MashtiHasanRestaurant.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Services.Interface
{
    public interface ICategoryService
    {
        Task<OperationResult> Delete(int ID);
        Task<FoodCategoryAddEditModel> Get(int ID);
        Task<OperationResult> Update(FoodCategoryAddEditModel Category);
        Task<List<NewsCategoryListItem>> GetAll();
        Task<bool> HasFoods(int ID);
    }
}

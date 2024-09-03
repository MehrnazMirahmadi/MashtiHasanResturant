using MashtiHasanRestaurant.Core.Common;
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
    public class CategoryService : ICategoryService
    {
        private readonly ResturantMashtiHasanContext _context ;
        public CategoryService(ResturantMashtiHasanContext context)
        {
                _context = context;
        }
        Category ToDBModel(FoodCategoryAddEditModel Category) { 
            return new Category{
            CategoryId = Category.CategoryId,
            CategoryName = Category.CategoryName,
            Description = Category.Description
            
            }; 
        }
        FoodCategoryAddEditModel ToViewModel(Category category) { 
            return new FoodCategoryAddEditModel {
                  CategoryId = category.CategoryId
                , CategoryName = category.CategoryName
                , Description = category.Description };
        }
        public async Task<OperationResult> Delete(int ID)
        {
            OperationResult op = new OperationResult();
            var cat =await _context.Category.FirstOrDefaultAsync(x=>x.CategoryId == ID);
            if (cat == null) {
                return op.ToFailed("Record Not Found");
            }
            if(await HasFoods(ID))
            {
                return op.ToFailed("Has Foods");
            }
            try { 
                _context.Category.Remove(cat);
                await _context.SaveChangesAsync();
                return op.ToSuccess("Deleted Successfully");
            }
            catch (Exception ex) {
                return op.ToFailed("Deleted Failed " + ex.Message);
            }
        }

        public async Task<FoodCategoryAddEditModel> Get(int ID)
        {
            var c = await _context.Category.FirstOrDefaultAsync(x=>x.CategoryId == ID);
            return ToViewModel(c);
        }

        public async Task<List<NewsCategoryListItem>> GetAll()
        {
            var cats =await _context.Category.Select(c => new NewsCategoryListItem
            {
                CategoryId  = c.CategoryId,
                CategoryName = c.CategoryName,
                FoodCount = c.Food.Count
            }).ToListAsync();
            return cats;
         
        }

        public async Task<bool> HasFoods(int ID)
        {
            return await _context.Food.AnyAsync(x => x.CategoryId == ID);
        }

        public async Task<OperationResult> Update(FoodCategoryAddEditModel Category)
        {
            var op = new OperationResult();
            var c = ToDBModel(Category);
            try
            {
                _context.Attach(c);
                _context.Entry<Category>(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return op.ToSuccess("Saved");
            }
            catch (Exception ex)
            {

                return op.ToFailed("Failed " + ex.Message);

            }
        }
     
        public async Task<OperationResult> Add(FoodCategoryAddEditModel cat)
        {
            var op = new OperationResult();
            var c = ToDBModel(cat);
            try
            {
                _context.Category.Add(c);
                await _context.SaveChangesAsync();
                return op.ToSuccess("Category Added Successfully");
            }
            catch (Exception ex)
            {

                return op.ToFailed("Added Category Failed " + ex.Message);
            }

        }

        public async Task<List<NewsCategoryListItem>> GetRoots()
        {
            var cats = await _context.Category.Select(x => new NewsCategoryListItem
            {
                CategoryName = x.CategoryName
                ,
                CategoryId = x.CategoryId
                ,
                FoodCount = x.Food.Count
               
            }).ToListAsync();
            return cats;
        }
    }
}

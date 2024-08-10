using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.DTOs
{
    public class FoodItemViewModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string Ingredients { get; set; }
        public int UnitPrice { get; set; }
        public string ImageAddress { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
  
    public class AddFoodViewModel
    {
       
        public Food Food { get; set; }
        public SelectList Cat { get; set; }
    }

}

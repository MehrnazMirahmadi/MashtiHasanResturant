using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.DTOs
{
    public class FoodListItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string Ingredients { get; set; }
        public int UnitPrice { get; set; }
        public string ImageAddress { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
       
       
    }
}

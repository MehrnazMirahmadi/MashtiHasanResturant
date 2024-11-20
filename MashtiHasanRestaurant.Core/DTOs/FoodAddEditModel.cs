using MashtiHasanRestaurant.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.DTOs
{
    public class FoodAddEditModel
    {
        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public int UnitPrice { get; set; }

        public string Ingredient { get; set; }

        public string ImageAddress { get; set; }

        public int CategoryId { get; set; }

        public int CategoryID { get; set; }
        public int SortOrder { get; set; }
    }
}

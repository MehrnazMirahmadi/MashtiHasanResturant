using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.DTOs
{
    public class ShowGroupViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int FoodCount { get; set; }
    }
}

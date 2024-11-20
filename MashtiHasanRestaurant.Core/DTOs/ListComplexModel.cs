using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.DTOs
{
    public class ListComplexModel
    {
        public List<FoodListItem> FoodsList { get; set; }
        public int RecordCount { get; set; }
    }
}

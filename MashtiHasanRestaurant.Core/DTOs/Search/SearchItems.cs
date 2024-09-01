using MashtiHasanRestaurant.Core.Common;


namespace MashtiHasanRestaurant.Core.DTOs.Search
{
    public class SearchItems : PageModel
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Family { get; set; }
        public string Tel { get; set; }
        public int? UnitPriceFrom{ get; set; }
        public int? UnitPriceTo {  get; set; }   
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }
}

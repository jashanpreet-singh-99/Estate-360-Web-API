namespace EState_360.Core.Entities
{
	public class ListingSearch
	{
        public required string Type { get; set; } = "Rent";
        public string? Keywords { get; set; }
        public string? Location { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}


namespace EState_360.Core.Entities
{
    public class Listing
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string? Id { get; set; } = null;

        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public required string Name { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "address")]
        public string Address { get; set; } = "";

        [Newtonsoft.Json.JsonProperty(PropertyName = "region")]
        public required string Region { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "pinCode")]
        public string PinCode { get; set; } = "";

        [Newtonsoft.Json.JsonProperty(PropertyName = "agent")]
        public string Agent { get; set; } = "";

        [Newtonsoft.Json.JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = "";

        [Newtonsoft.Json.JsonProperty(PropertyName = "img")]
        public string Img { get; set; } = "";

        [Newtonsoft.Json.JsonProperty(PropertyName = "beds")]
        public int Beds { get; set; } = 1;

        [Newtonsoft.Json.JsonProperty(PropertyName = "baths")]
        public int Baths { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty(PropertyName = "area")]
        public int Area { get; set; } = 1;

        [Newtonsoft.Json.JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = "Rent";

        [Newtonsoft.Json.JsonProperty(PropertyName = "rating")]
        public decimal Rating { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty(PropertyName = "ratingCount")]
        public int RatingCount { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty(PropertyName = "postedOn")]
        public DateTime PostedOn { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "price")]
        public int Price { get; set; } = 1;
    }
}
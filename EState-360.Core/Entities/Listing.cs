namespace EState_360.Core.Entities
{
    public class Listing
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public required string Id { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = "";
        [Newtonsoft.Json.JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}
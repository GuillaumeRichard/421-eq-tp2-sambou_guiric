namespace CityPoiAPI.DTO
{
    public class PointOfInterestDto
    {
        //YM: devrait contenir des validations + validation testées
        public int Id {get; set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public string Longitude { get; set; } 
        public string Latitude { get; set;}
        public string ImageUrl { get; set; }
    }
}

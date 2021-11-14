namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddCarrierDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public double MaxCargoVolume { get; set; }
    }
}

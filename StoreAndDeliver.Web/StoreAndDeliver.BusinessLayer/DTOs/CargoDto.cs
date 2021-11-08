using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}

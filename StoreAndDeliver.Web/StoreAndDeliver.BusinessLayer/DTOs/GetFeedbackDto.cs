using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class GetFeedbackDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
    }
}

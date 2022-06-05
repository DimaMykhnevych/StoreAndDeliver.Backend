using System;
using System.ComponentModel.DataAnnotations;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public Guid AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}

using StoreAndDeliver.DataLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CarryOutBefore { get; set; }
        public DateTime? StoreFromDate { get; set; }
        public DateTime? StoreUntilDate { get; set; }

        [Required]
        public RequestType Type { get; set; }
        public bool IsSecurityModeEnabled { get; set; }

        [Required]
        public decimal TotalSum { get; set; }
        public Guid FromAddressId { get; set; }
        public Guid? ToAddressId { get; set; }
        public Guid AppUserId { get; set; }
        public AddressDto FromAddress { get; set; }
        public AddressDto ToAddress { get; set; }
    }
}

using DatawareHouse.Models.Enums;

namespace DatawareHouse.Models.Entities
{
    public class InventoryMismatch
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public string? DetectedPositionCode { get; set; }
        public MismatchType MismatchType { get; set; }
        public DateTime DetectedAt { get; set; }
    }
}
using DatawareHouse.Models.Enums;

namespace DatawareHouse.Models.Entities
{
    public class SuggestedPlacement
    {
        public Guid Id { get; set; }
        public string SuggestedPositionCode { get; set; } = string.Empty;
        public Guid PartId { get; set; }
        public SuggestionAlgorithm AlgorithmUsed { get; set; }
        public float FitScore { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
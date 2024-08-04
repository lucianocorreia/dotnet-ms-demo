using System.Diagnostics.CodeAnalysis;

namespace AuctionService.DTOs;

public class UpdateAuctionDto
{
    public string Make { get; set; } = default!;
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Color { get; set; }
    public int? Mileage { get; set; }
}

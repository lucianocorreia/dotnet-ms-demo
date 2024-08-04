using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AuctionDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
            .Include(a => a.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();

        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound();
        }

        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
    {
        var auction = _mapper.Map<Auction>(createAuctionDto);

        // TODO*: Add current user as seller
        auction.Seller = "teste";

        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to create auction");
        }

        return CreatedAtAction(
            nameof(GetAuctionById),
            new { id = auction.Id },
            _mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        Console.WriteLine(">>>>>>>>>>>> " + auction);

        if (auction == null)
        {
            return NotFound("Auction not found");
        }

        // TODO*: Check if current user is the seller

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

        var result = await _context.SaveChangesAsync();

        // if (!result)
        // {
        //     return BadRequest("Failed to update auction");
        // }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult?> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);

        if (auction == null)
        {
            return NotFound("Auction not found");
        }

        // TODO*: Check if current user is the seller

        _context.Auctions.Remove(auction);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

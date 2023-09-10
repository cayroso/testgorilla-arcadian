using Data.DbContexts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiAngular.Extensions;
using WebApiAngular.Models.Requests;
using WebApiAngular.Models.Responses;

namespace WebApiAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TransactionController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var dto = await dbContext.Transactions
            .AsNoTracking()
            .Select(e => new GetTransactions
            {
                Id = e.TransactionId,
                Name = e.Name,
                Date = e.Date.AsUtc(),
                Cost = e.Cost
            })
            .ToListAsync(cancellationToken);

        return Ok(dto);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] AppDbContext dbContext,
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var dto = await dbContext.Transactions
            .AsNoTracking()
            .Where(e => e.TransactionId == id)
            .Select(e => new GetTransactionById
            {
                Id = e.TransactionId,
                Name = e.Name,
                Date = e.Date.AsUtc(),
                Cost = e.Cost
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (dto is null)
            return NotFound($"Transaction with Id=\"{id}\" is not found.");

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostCreatAsync(
        [FromServices] AppDbContext dbContext,
        [FromBody] AddTransaction req,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Problem();

        var data = new Transaction
        {
            Name = req.Name,
            Date = req.Date.AsUtc().Truncate(),
            Cost = req.Cost,
        };

        await dbContext.AddAsync(data, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(data.TransactionId);
    }

}

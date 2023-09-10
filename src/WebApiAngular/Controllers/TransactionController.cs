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
    private readonly AppDbContext _dbContext;

    public TransactionController(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var dto = await _dbContext.Transactions
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
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var dto = await _dbContext.Transactions
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
    public async Task<IActionResult> CreateAsync(
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

        await _dbContext.AddAsync(data, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(data.TransactionId);
    }


    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> DeleteByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var data = await _dbContext.Transactions
            .Where(e => e.TransactionId == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (data is null)
            return NotFound($"Transaction with Id=\"{id}\" is not found.");

        _dbContext.Remove(data);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdatedAsync(
       [FromBody] UpdateTransaction req,
       CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Problem();

        var data = await _dbContext.Transactions
            .Where(e => e.TransactionId == req.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (data is null)
            return NotFound($"Transaction with Id=\"{req.Id}\" is not found.");

        data.Name = req.Name;
        data.Date = req.Date.AsUtc().Truncate();
        data.Cost = req.Cost;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(data.TransactionId);
    }

}

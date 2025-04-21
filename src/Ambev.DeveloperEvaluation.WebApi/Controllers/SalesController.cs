using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ILogger<SalesController> _logger;
    private readonly IEventPublisher _eventPublisher;

    public SalesController(
        ISaleService saleService,
        ILogger<SalesController> logger,
        IEventPublisher eventPublisher)
    {
        _saleService = saleService;
        _logger = logger;
        _eventPublisher = eventPublisher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        try
        {
            var sales = await _saleService.GetAll();
            return Ok(sales);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all sales");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSale(int id)
    {
        try
        {
            var sale = await _saleService.GetById(id);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting sale with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] SaleDto saleDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _saleService.Create(saleDto);

            await _eventPublisher.Publish(new SaleCreatedEvent(
                saleDto.Id,
                saleDto.ClientName,
                saleDto.Total));

            return CreatedAtAction(nameof(GetSale), new { id = saleDto.Id }, saleDto);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Business rule violation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating sale");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] SaleDto saleDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != saleDto.Id)
                return BadRequest("ID mismatch");

            await _saleService.Update(saleDto);

            await _eventPublisher.Publish(new SaleModifiedEvent(
              saleDto.Id,
              User.Identity?.Name ?? "System",
              saleDto.Total));

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Business rule violation");
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating sale with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}/cancel/status")]
    public async Task<IActionResult> CancelSale(int id)
    {
        try
        {
            var sale = await _saleService.GetById(id);
           
            await _saleService.CancelSale(id);

            var action = sale.IsCanceled ? "canceled" : "uncanceled";
            var eventMessage = sale.IsCanceled
                ? $"Sale Canceled by {User.Identity?.Name ?? "System"}"
                : $"Sale Uncanceled by {User.Identity?.Name ?? "System"}";

            await _eventPublisher.Publish(new SaleCanceledEvent(id, eventMessage));

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error canceling sale with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(int? id)
    {
        try
        {
            if (id == null) return NotFound();

            await _saleService.DeleteSale(id);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting sale with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

   
}
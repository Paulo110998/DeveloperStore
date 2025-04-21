using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesItemController : ControllerBase
{
    private readonly ISaleItemService _saleItemService;
    private readonly ILogger<SalesController> _logger;
    private readonly IEventPublisher _eventPublisher;
    private readonly ISaleService _saleService;

    public SalesItemController(ISaleItemService saleItemService, ILogger<SalesController> logger,
        IEventPublisher eventPublisher, ISaleService saleService)
    {
        _saleItemService = saleItemService;
        _logger = logger;
        _eventPublisher = eventPublisher;
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSaleItems()
    {
        try
        {
            var items = await _saleItemService.GetAll();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all sale items");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleItem(int id)
    {
        try
        {
            var item = await _saleItemService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting sale item with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSaleItem([FromBody] SaleItemDto itemDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _saleItemService.Create(itemDto);

            return CreatedAtAction(nameof(GetSaleItem), new { id = itemDto.Id }, itemDto);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Business rule violation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating sale item");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSaleItem(int id, [FromBody] SaleItemDto itemDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != itemDto.Id)
                return BadRequest("ID mismatch");

            await _saleItemService.Update(itemDto);

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
            _logger.LogError(ex, $"Error updating sale item with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}/cancelItem/status")]
    public async Task<IActionResult> CancelSaleItem(int id)
    {
        try
        {
            var item = await _saleItemService.GetById(id);
          
            await _saleItemService.CancelSaleItem(id);

            var action = item.IsCanceled ? "canceled" : "uncanceled";
            var eventMessage = item.IsCanceled
                ? $"Sale Item Canceled by {User.Identity?.Name ?? "System"}"
                : $"Sale Item Uncanceled by {User.Identity?.Name ?? "System"}";

            // Publicar evento
            await _eventPublisher.Publish(new SaleItemCanceledEvent(
                item.SaleId,
                item.Id,
                item.ProductName,
                "User requested item cancellation"));

            return NoContent();

        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error canceling saleItem with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSaleItem(int id)
    {
        try
        {
            var item = await _saleItemService.GetById(id);
            if (item == null)
                return NotFound();

            await _saleItemService.DeleteSaleItem(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error canceling sale item with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}

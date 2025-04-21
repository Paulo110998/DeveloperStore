
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int SaleId { get; set; }
    public virtual Sale? Sale { get; set; }

    [Required]
    public string? ProductName { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public decimal Discount { get;  set; }

    public bool IsCanceled { get; set; }

    [Required]
    public decimal Total => Quantity * UnitPrice - Discount;

    public void Cancel()
    {
        IsCanceled = !IsCanceled;
    }

    public void ApplyDiscount()
    {
        if (IsCanceled) return;

        if (Quantity > 20) 
            throw new InvalidOperationException("Invalid quantity for discount");

        Discount = Quantity switch
        {
            >= 10 and <= 20 => Quantity * UnitPrice * 0.20m,
            >= 4 => Quantity * UnitPrice * 0.10m,
            _ => 0
        };
    }
}

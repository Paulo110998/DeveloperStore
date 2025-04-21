

using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string? SaleNumber { get; set; } 

    [Required]
    public DateTime SaleDate { get; set; } 

    [Required]
    public string? ClientName { get; set; } 

    [Required]
    public string? Branch { get; set; }

    public decimal Total => Items.Where(i => !i.IsCanceled).Sum(i => i.Total);

    public bool IsCanceled { get; set; }

    public virtual ICollection<SaleItem> Items {  get; set; }

    public void Cancel()
    {
        IsCanceled = !IsCanceled;
    }

    public void AddItem(SaleItem item)
    {
        if (IsCanceled)
            throw new InvalidOperationException("Cannot add items to canceled sale");

        if (item.Quantity > 20)
            throw new InvalidOperationException("Maximum quantity per item entry is 20");

        var existingQuantity = Items
            .Where(i => i.ProductName == item.ProductName && !i.IsCanceled)
            .Sum(i => i.Quantity);

        if (existingQuantity + item.Quantity > 20)
            throw new InvalidOperationException(
                $"Total quantity for product {item.ProductName} cannot exceed 20 units. " +
                $"Current: {existingQuantity}, Adding: {item.Quantity}");

        item.ApplyDiscount();
        Items.Add(item);
    }
}

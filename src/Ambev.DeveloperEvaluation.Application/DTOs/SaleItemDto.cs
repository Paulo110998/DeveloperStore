using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.DTOs;

public class SaleItemDto
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int SaleId { get; set; }

    [Required]
    public string? ProductName { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public bool IsCanceled { get; set; }

    [Required]
    public decimal Total { get; set; }
}
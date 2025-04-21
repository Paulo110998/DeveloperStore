using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.DTOs;

public class SaleDto
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
    public string Branch { get; set; }
    
    public decimal Total { get; set; }
    
    public bool IsCanceled { get; set; }
    
    public ICollection<SaleItemDto> Items { get; set; }
}
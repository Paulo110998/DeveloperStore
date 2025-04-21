using Ambev.DeveloperEvaluation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;

public interface ISaleItemService
{
    Task<IEnumerable<SaleItemDto>> GetAll();
    Task<SaleItemDto> GetById(int? id);
    Task Create(SaleItemDto dto);
    Task Update(SaleItemDto dto);
    Task CancelSaleItem(int id);
    Task DeleteSaleItem(int? id);
}

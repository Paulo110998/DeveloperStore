using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<SaleItem> GetByIdAsync(int? id);
    Task<IEnumerable<SaleItem>> GetAllAsync();
    Task<SaleItem> AddAsync(SaleItem saleItem);
    Task<SaleItem> UpdateAsync(SaleItem saleItem);
    Task<SaleItem> DeleteAsync(SaleItem saleItem);
}

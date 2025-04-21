using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> AddAsync(SaleItem saleItem)
    {
        _context.Add(saleItem);
        await _context.SaveChangesAsync();
        return saleItem;
    }

    public async Task<SaleItem> DeleteAsync(SaleItem saleItem)
    {
       _context.Remove(saleItem);
        await _context.SaveChangesAsync();
        return saleItem;
    }

    public async Task<IEnumerable<SaleItem>> GetAllAsync()
    {
        return await _context.SaleItems.ToListAsync();
    }

    public async Task<SaleItem> GetByIdAsync(int? id)
    {
        return await _context.SaleItems.FindAsync(id);
    }

    public async Task<SaleItem> UpdateAsync(SaleItem saleItem)
    {
        _context.Update(saleItem);
        await _context.SaveChangesAsync();
        return saleItem;
    }
}

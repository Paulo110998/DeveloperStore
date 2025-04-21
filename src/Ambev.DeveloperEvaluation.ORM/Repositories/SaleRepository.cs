using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> AddAsync(Sale sale)
    {
        _context.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale> DeleteAsync(Sale sale)
    {
        _context.Remove(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync();
    }

    public async Task<Sale> GetByIdAsync(int? id)
    {
        return await _context.Sales
            .Include(s => s.Items) 
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale> UpdateAsync(Sale sale)
    {
        _context.Update(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(int? id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> AddAsync(Sale sale);
    Task<Sale> UpdateAsync(Sale sale);
    Task<Sale> DeleteAsync(Sale sale);
  
}

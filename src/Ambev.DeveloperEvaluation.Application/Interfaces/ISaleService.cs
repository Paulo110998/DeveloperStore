using Ambev.DeveloperEvaluation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;


public interface ISaleService
{
    Task<IEnumerable<SaleDto>> GetAll();
    Task<SaleDto> GetById(int? id);
    Task Create(SaleDto dto);
    Task Update(SaleDto dto);
    Task CancelSale(int id);
    Task DeleteSale(int? id);

}
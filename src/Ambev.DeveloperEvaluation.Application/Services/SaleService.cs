using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public SaleService(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleDto>> GetAll()
    {
        var sale = await _saleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SaleDto>>(sale);
        
    }

    public async Task<SaleDto?> GetById(int? id)
    {
        if (id == null) return null; 

        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null) return null; 

        return _mapper.Map<SaleDto>(sale);
    }

    public async Task Create(SaleDto dto)
    {
        var sale = _mapper.Map<Sale>(dto);
        await _saleRepository.AddAsync(sale);
    }

    public async Task Update(SaleDto dto)
    {
        var existingSale = await _saleRepository.GetByIdAsync(dto.Id);
        if (existingSale == null) throw new KeyNotFoundException();

        _mapper.Map(dto, existingSale); 
        await _saleRepository.UpdateAsync(existingSale);
    }

    public async Task CancelSale(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null) throw new KeyNotFoundException("Sale not found");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale);
    }

    public async Task DeleteSale(int? id)
    {
        var sale = _saleRepository.GetByIdAsync(id).Result;
        await _saleRepository.DeleteAsync(sale);    
    }
}
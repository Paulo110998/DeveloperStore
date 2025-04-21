using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SaleItemService : ISaleItemService
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;

    public SaleItemService(ISaleItemRepository saleItemRepository, 
        IMapper mapper)
    {
        _saleItemRepository = saleItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleItemDto>> GetAll()
    {
       var saleItem = await _saleItemRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SaleItemDto>>(saleItem);
    }

    public async Task<SaleItemDto> GetById(int? id)
    {
        var saleItem = await _saleItemRepository.GetByIdAsync(id);
        return _mapper.Map<SaleItemDto>(saleItem);
    }

    public Task Create(SaleItemDto dto)
    {
        var saleItem = _mapper.Map<SaleItem>(dto);
        return _saleItemRepository.AddAsync(saleItem);
    }

    public Task Update(SaleItemDto dto)
    {
        var saleItem = _mapper.Map<SaleItem>(dto);
        return _saleItemRepository.UpdateAsync(saleItem);
    }

    public async Task CancelSaleItem(int id)
    {
        var saleItem = await _saleItemRepository.GetByIdAsync(id);
        if (saleItem == null) throw new KeyNotFoundException("Sale not found");

        saleItem.Cancel();
        await _saleItemRepository.UpdateAsync(saleItem);
    }

    public async Task DeleteSaleItem(int? id)
    {
        var saleItem = _saleItemRepository.GetByIdAsync(id).Result;
        await _saleItemRepository.DeleteAsync(saleItem);
    }
}

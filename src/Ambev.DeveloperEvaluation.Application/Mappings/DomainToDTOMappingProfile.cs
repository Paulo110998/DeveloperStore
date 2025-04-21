 using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Mappings;

//public class DomainToDTOMappingProfile : Profile
//{
//    public DomainToDTOMappingProfile()
//    {
//        CreateMap<Sale, SaleDto>().ForMember(dto =>
//        dto.Items, opt => opt.MapFrom(entity => entity.Items))
//            .ReverseMap();

//        CreateMap<SaleItem, SaleItemDto>().ReverseMap();
//    }
//}

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Sale, SaleDto>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ReverseMap();

        CreateMap<SaleItem, SaleItemDto>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ReverseMap();
    }
}
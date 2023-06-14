using AutoMapper;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Models;

namespace Proprette.Domain.Data.Profiles
{
    internal class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            CreateMap<Warehouse, WarehouseDto>()
                .ForMember(s => s.ItemName, opt => opt.MapFrom(s => s.Item.ItemName))
                .ForMember(s => s.ItemType, opt => opt.MapFrom(s => s.Item.ItemType))
                .ReverseMap();
        }
    }
}

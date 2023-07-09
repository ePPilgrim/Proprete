using AutoMapper;
using Proprette.Domain.Data.Entities;

namespace Proprette.DataSeeding.DataSource.Models
{
    internal class FileDomainProfile  : Profile
    {
        public FileDomainProfile() {
            CreateMap<Warehouse, FileToWarehouse>()
                .ForMember(s => s.ItemName, opt => opt.MapFrom(s => s.Item.ItemName))
                .ForMember(s => s.ItemType, opt => opt.MapFrom(s => s.Item.ItemType))
                .ReverseMap();
        }
    }
}

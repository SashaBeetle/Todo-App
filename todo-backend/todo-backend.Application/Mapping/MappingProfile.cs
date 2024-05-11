using AutoMapper;
using todo_backend.Domain.Models;
using todo_backend.Application.Mapping.DTOs;

namespace todo_backend.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDTO>();
            CreateMap<HistoryItem, HistoryItemDTO>();
            CreateMap<Catalog, CatalogDTO>();

            CreateMap<CardDTO, Card>();
            CreateMap<HistoryItemDTO, HistoryItem>();
            CreateMap<CatalogDTO, Catalog>();
        }
    }
}

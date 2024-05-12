using AutoMapper;
using todo_backend.Domain.Models;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDTO>();
            CreateMap<HistoryItem, HistoryItemDTO>();
            CreateMap<Catalog, CatalogDTO>();
            CreateMap<Board, BoardDTO>();

            CreateMap<CardDTO, Card>();
            CreateMap<HistoryItemDTO, HistoryItem>();
            CreateMap<CatalogDTO, Catalog>();
            CreateMap<BoardDTO, Board>();
        }
    }
}

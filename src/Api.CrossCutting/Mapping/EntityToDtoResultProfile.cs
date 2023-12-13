using Api.Domain.DTOs;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mapping;

public class EntityToDtoResultProfile : Profile
{
    public EntityToDtoResultProfile()
    {
        CreateMap<StarshipEntity, StarshipDtoResult>()
        .AfterMap((src, dest) =>
        {
            dest.CreatedAt = FormatDateTime(src.CreatedAt);
            dest.UpdatedAt = FormatDateTime(src.UpdatedAt);
        });
    }

    public string? FormatDateTime(DateTime? source)
    {
        return source?.ToString("dd/MM/yyyy hh:mm:ss");
    }
}
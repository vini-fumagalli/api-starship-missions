using Api.Domain.DTOs;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mapping;

public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        CreateMap<StarshipUpdateDto, StarshipEntity>();
    }
}
using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.DTOs.Starship;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mapping;

//classe que contém construtor que implementa 
//Mapping de DTO(Data Transfer Object) para Entity
//com suas respectivas regras de negócio
public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        CreateMap<StarshipUpdateDto, StarshipEntity>()
        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow.ToLocalTime()))
        .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model!.Replace(" ", ".").ToUpper()))
        .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer!.Replace(" ", ".").ToUpper()));

        CreateMap<StarshipCreateDto, StarshipEntity>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name!.Replace(" ", ".").ToUpper()))
        .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model!.Replace(" ", ".").ToUpper()))
        .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer!.Replace(" ", ".").ToUpper()))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow.ToLocalTime()));

        CreateMap<MissionsCreateDto, MissionsEnitity>()
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow.ToLocalTime()));
    }
}
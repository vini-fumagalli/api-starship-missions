using Api.Domain.DTOs;
using Api.Domain.DTOs.Starship;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

//Interface do contrato de métodos que serão implementados
//por StarshipService
public interface IStarshipService
{
    Task<ResponseEntity> CreateStarship(List<string> starshipModels);
    Task<ResponseEntity> GetStarshipByName(string name);
    Task<ResponseEntity> GetStarshipByModel(string model);
    Task<ResponseEntity> GetStarshipByManufacturer(string manufacturer);
    Task<ResponseEntity> UpdateStarship(string name, StarshipUpdateDto starship);
    Task<ResponseEntity> DeleteStarship(string name);

}
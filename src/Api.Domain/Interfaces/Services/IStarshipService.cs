using Api.Domain.DTOs;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IStarshipService
{
    Task<ResponseEntity> CreateStarship(List<string> starshipModels);
    Task<ResponseEntity> GetStarshipByName(string name);
    Task<ResponseEntity> GetStarshipByModel(string model);
    Task<ResponseEntity> GetStarshipByManufacturer(string manufacturer);
    Task<ResponseEntity> UpdateStarship(string name, StarshipUpdateDto starship);
    Task<ResponseEntity> DeleteStarship(string name);

}
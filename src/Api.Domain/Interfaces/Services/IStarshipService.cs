using Api.Domain.DTOs;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IStarshipService
{
    Task<ResponseEntity> Create(List<string> starshipModels);
    Task<ResponseEntity> GetByName(string name);
    Task<ResponseEntity> GetByModel(string model);
    Task<ResponseEntity> GetByManufacturer(string manufacturer);
    Task<ResponseEntity> Update(string name, StarshipUpdateDto starship);
    Task<ResponseEntity> Delete(string name);

}
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IStarshipAndMissionsRepository
{
    Task<List<StarshipEntity>> GetStarships();
    Task<StarshipEntity?> GetStarshipByName(string name);
    Task<StarshipEntity?> UpdateStarship(string name, StarshipEntity starship);
    Task<bool> DeleteStarship(string name);
    Task<StarshipEntity?> GetStarshipByModel(string model);
    Task<StarshipEntity?> GetStarshipByManufacturer(string manufacturer);
    Task<StarshipEntity?> CreateStarship(StarshipEntity starship);

}
using Api.Domain.DTOs;
using Api.Domain.DTOs.StarshipMissions;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IStarshipAndMissionsRepository
{
    Task<List<StarshipEntity>> GetStarships();
    Task<StarshipEntity?> GetStarshipByName(string name);
    Task<StarshipEntity?> UpdateStarship(StarshipEntity starship);
    Task<bool> DeleteStarship(string name);
    Task<StarshipEntity?> GetStarshipByModel(string model);
    Task<StarshipEntity?> GetStarshipByManufacturer(string manufacturer);
    Task<List<StarshipEntity>> CreateStarship(List<StarshipEntity> starships);
    Task<StarshipMissionsDto> GetMissionsByStarship(string starshipName);
    Task<List<MissionsEnitity>> CreateMission(List<MissionsEnitity> missions);

}
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IMissionsService
{
    Task<ResponseEntity> GetMissions();
    Task<ResponseEntity> GetMissionsByStarship(string starshipName);
    Task<ResponseEntity> CreateMission(MissionsEnitity mission);
}
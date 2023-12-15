using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IMissionsService
{
    Task<ResponseEntity> GetMissions();
    Task<ResponseEntity> GetMissionsByStarship(string starshipName);
    Task<ResponseEntity> CreateMission(MissionsCreateDto mission);
}
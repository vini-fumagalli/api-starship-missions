using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

//Interface do contrato de métodos que serão implementados
//por MissionsService
public interface IMissionsService
{
    Task<ResponseEntity> GetMissions();
    Task<ResponseEntity> GetMissionsByStarship(string starshipName);
    Task<ResponseEntity> CreateMission(List<string> starshipNames, MissionsCreateDto mission);
}
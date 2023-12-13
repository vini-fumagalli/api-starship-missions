using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;

namespace Api.Service.Services;

public class MissionsService : IMissionsService
{
    private readonly IStarshipAndMissionsRepository _repository;

    public MissionsService(IStarshipAndMissionsRepository repository)
    {
        _repository = repository;
    }

    public Task<ResponseEntity> CreateMission(MissionsEnitity mission)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseEntity> GetMissions()
    {
        var starshipList = await _repository.GetStarships();
        var starshipMissionsList = new List<StarshipMissionsDto>();

        foreach (var starship in starshipList)
        {
            var missionsByStarship = await _repository.GetMissionsByStarship(starship.Name!);

            starshipMissionsList.Add(missionsByStarship);
        }

        return new ResponseEntity
        {
            Success = starshipMissionsList.Any(),
            Response = starshipMissionsList
        };
    }

    public async Task<ResponseEntity> GetMissionsByStarship(string starshipName)
    {
        var response = await _repository.GetMissionsByStarship(starshipName);

        return new ResponseEntity
        {
            Success = true,
            Response = response 
        };
    }
}
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
    public async Task<ResponseEntity> GetMissions()
    {
        var starshipList = await _repository.GetStarships(); 
        var starshipMissionsList = new List<StarshipMissionsDto>();

        foreach(var starship in starshipList)
        {
            var missionsByStarship = await _repository.GetMissionsByStarship(starship.Name!);

            starshipMissionsList.Add(missionsByStarship);
        }

        if(starshipMissionsList.Count == 0)
        {
            return new ResponseEntity
            {
                Success = false,
                Response = starshipMissionsList
            };    
        }
        
        return new ResponseEntity
        {
            Success = true,
            Response = starshipMissionsList
        };
    }


}
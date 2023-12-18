using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.DTOs.Starship;
using Api.Domain.DTOs.StarshipMissions;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class MissionsService : IMissionsService
{
    private readonly IStarshipAndMissionsRepository _repository;
    private readonly IMapper _mapper;

    public MissionsService(IStarshipAndMissionsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseEntity> CreateMission(List<string> starshipNames, MissionsCreateDto mission)
    {
        var missionsList = new List<MissionsEnitity>();

        foreach(var starshipName in starshipNames)
        {
            var existsStarship = await _repository.GetStarshipByName(starshipName.Replace(" ", ".").ToUpper());
            if(existsStarship != null)
            {
                var missionsEntity = _mapper.Map<MissionsEnitity>(mission);
                missionsEntity.StarshipName = starshipName.Replace(" ", ".").ToUpper();
                missionsList.Add(missionsEntity);    
            }
        }

        var response = await _repository.CreateMission(missionsList);
        var formattedResponse = _mapper.Map<List<MissionsDtoResult>>(response);

        return new ResponseEntity
        {
            Success = formattedResponse.Any(),
            Response = formattedResponse
        };
    }

    public async Task<ResponseEntity> GetMissions()
    {
        var starshipList = await _repository.GetStarships();
        var starshipMissionsList = new List<StarshipMissionsDtoResult>();

        foreach (var starship in starshipList)
        {
            var starshipMissionsDto = await _repository.GetMissionsByStarship(starship.Name!);

            var missionsByStarship = MapToStarshipMissionsDtoResult(starshipMissionsDto.Starship!,
                                                                    starshipMissionsDto.Missions!);

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
        starshipName = starshipName.Replace(" ", ".").ToUpper();
        var starshipMissionsDto = await _repository.GetMissionsByStarship(starshipName);

        var response = MapToStarshipMissionsDtoResult(starshipMissionsDto.Starship!,
                                                      starshipMissionsDto.Missions!);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    private StarshipMissionsDtoResult MapToStarshipMissionsDtoResult
    (StarshipEntity starship, List<MissionsEnitity> missions)
    {
        var formattedStarship = _mapper.Map<StarshipDtoResult>(starship);
        var formattedMissions = _mapper.Map<List<MissionsDtoResult>>(missions);

        return new StarshipMissionsDtoResult
        {
            Starship = formattedStarship,
            Missions = formattedMissions
        };
    }
}
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

    public async Task<ResponseEntity> CreateMission(MissionsCreateDto mission)
    {
        var missionEnitity = _mapper.Map<MissionsEnitity>(mission);
        var missionToCreate = await _repository.CreateMission(missionEnitity);
        var response = _mapper.Map<MissionsDtoResult>(missionToCreate);

        return new ResponseEntity
        {
            Success = true,
            Response = response
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
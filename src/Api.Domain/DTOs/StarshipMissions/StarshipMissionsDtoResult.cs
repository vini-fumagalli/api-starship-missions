using Api.Domain.DTOs.Missions;
using Api.Domain.DTOs.Starship;

namespace Api.Domain.DTOs.StarshipMissions;

public class StarshipMissionsDtoResult
{
    public StarshipDtoResult? Starship { get; set; }
    public List<MissionsDtoResult>? Missions { get; set; }
}
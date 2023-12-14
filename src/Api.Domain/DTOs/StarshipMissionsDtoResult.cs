namespace Api.Domain.DTOs;

public class StarshipMissionsDtoResult
{
    public StarshipDtoResult? Starship { get; set; }
    public List<MissionsDtoResult>? Missions { get; set; }
}
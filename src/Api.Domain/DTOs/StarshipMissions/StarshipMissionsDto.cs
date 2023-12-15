using Api.Domain.Entities;

namespace Api.Domain.DTOs.StarshipMissions;

public class StarshipMissionsDto
{
    public StarshipEntity? Starship { get; set; }
    public List<MissionsEnitity>? Missions { get; set; }

}
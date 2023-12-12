using Api.Domain.Entities;

namespace Api.Domain.DTOs;

public class StarshipMissionsDto
{
    public StarshipEntity? Starship { get; set; }
    public List<MissionsEnitity>? Missions { get; set; }

}
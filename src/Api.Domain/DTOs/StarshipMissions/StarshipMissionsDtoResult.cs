using Api.Domain.DTOs.Missions;
using Api.Domain.DTOs.Starship;

namespace Api.Domain.DTOs.StarshipMissions;

//Essa DTO(Data Trasfer Object) foi criada com
//intuito de formatar alguns dados StarshipMissionsDto 
//e posteriormente exibir aos usuários 
public class StarshipMissionsDtoResult
{
    public StarshipDtoResult? Starship { get; set; }
    public List<MissionsDtoResult>? Missions { get; set; }
}
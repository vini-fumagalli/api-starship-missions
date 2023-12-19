using Api.Domain.Entities;

namespace Api.Domain.DTOs.StarshipMissions;

//Essa DTO(Data Trasfer Object) foi criada com
//intuito de exibir a espaçonave em conjunto com
//cada missão na qual essa espaçonave estava presente.
//Ela contém um um objeto de StarshipEntity e uma Lista de 
//objetos de MissionsEntity  
public class StarshipMissionsDto
{
    public StarshipEntity? Starship { get; set; }
    public List<MissionsEnitity>? Missions { get; set; }

}
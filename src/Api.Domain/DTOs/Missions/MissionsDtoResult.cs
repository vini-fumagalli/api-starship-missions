namespace Api.Domain.DTOs.Missions;

public class MissionsDtoResult
{
    //DTO(Data Transfer Object) para exibir no corpo do
    //JSON para o usuário
    public string? StarshipName { get; set; }
    public string? Goal { get; set; }
    public string? Date { get; set; }
    public string? Duration { get; set; }
    public int? Crew { get; set; }
    public string? CreatedAt { get; set; }
}
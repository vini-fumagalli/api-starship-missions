namespace Api.Domain.DTOs.Missions;

//DTO(Data Transfer Object) para restringir o
//usuário a inserir apenas esses atributos de Missions
//quando usar Create, já que o Id será gerado pelo BD e StarshipName
//vem de uma lista de strings na requisição
public class MissionsCreateDto
{
    public string? Goal { get; set; }
    public string? Date { get; set; }
    public string? Duration { get; set; }
    public int? Crew { get; set; }
}
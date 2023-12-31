namespace Api.Domain.DTOs.Starship;


//DTO(Data Transfer Object) para realizar 
//Update de Starship. A DTO não possui o nome
//pois ele é pego separadamente na url da requisição
public class StarshipUpdateDto
{
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public string? CostInCredidts { get; set; }
    public string? Length { get; set; }
    public string? MaxAtmospheringSpeed { get; set; }
    public string? Crew { get; set; }
    public string? Passengers { get; set; }
    public string? CargoCapacity { get; set; }
    public string? Consumables { get; set; }
    public string? HyperdriveRating { get; set; }
    public string? Mglt { get; set; }
    public string? Class { get; set; }

}
using System.Text.Json.Serialization;

namespace Api.Domain.DTOs.Starship;

//DTO(Data Transfer Object) para trasnferir dados de 
//Starship quando usar o Create. Cada atributo tem uma 
//JsonPropertyName para indicar onde cada atributo do JSON
//da Api SwapiDev deve alocar seus dados
public class StarshipCreateDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("cost_in_credits")]
    public string? CostInCredidts { get; set; }

    [JsonPropertyName("length")]
    public string? Length { get; set; }

    [JsonPropertyName("max_atmosphering_speed")]
    public string? MaxAtmospheringSpeed { get; set; }

    [JsonPropertyName("crew")]
    public string? Crew { get; set; }

    [JsonPropertyName("passengers")]
    public string? Passengers { get; set; }

    [JsonPropertyName("cargo_capacity")]
    public string? CargoCapacity { get; set; }

    [JsonPropertyName("consumables")]
    public string? Consumables { get; set; }

    [JsonPropertyName("hyperdrive_rating")]
    public string? HyperdriveRating { get; set; }

    [JsonPropertyName("MGLT")]
    public string? Mglt { get; set; }

    [JsonPropertyName("starship_class")]
    public string? Class { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
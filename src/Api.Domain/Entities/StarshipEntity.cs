using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Domain.Entities;

public class StarshipEntity : BaseEntity
{
    [Key]
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


}
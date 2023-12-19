using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Domain.Entities;

public class StarshipEntity
{
    //Name é a primary key
    [Key]
    public string? Name { get; set; }
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
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


}
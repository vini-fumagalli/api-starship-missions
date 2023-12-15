using System.Text.Json.Serialization;
using Api.Domain.DTOs.Starship;

namespace Api.Domain.DTOs;

public class StarshipSearchResponseDto
{
    [JsonPropertyName("results")]
    public List<StarshipCreateDto>? Results { get; set; }
}

using System.Text.Json.Serialization;

namespace Api.Domain.DTOs;

public class StarshipSearchResponseDto
{
    [JsonPropertyName("results")]
    public List<StarshipCreateDto>? Results { get; set; }
}

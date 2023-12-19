using System.Text.Json.Serialization;
using Api.Domain.DTOs.Starship;

namespace Api.Domain.DTOs;

//DTO(Data Transfer Object) criada para receber a
//deserialização do JSON de uma espaçonave da Api 
//SwapiDev, já que na Api cada endpoint devolve uma lista de
//results contendo os atributos a serem alocados em StarshipCreateDto
public class StarshipSearchResponseDto
{
    [JsonPropertyName("results")]
    public List<StarshipCreateDto>? Results { get; set; }
}

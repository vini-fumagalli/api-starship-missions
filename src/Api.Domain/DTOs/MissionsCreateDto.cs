namespace Api.Domain.DTOs;

public class MissionsCreateDto
{
    public string? StarshipName { get; set; }
    public string? Goal { get; set; }
    public string? Date { get; set; }
    public string? Duration { get; set; }
    public int? Crew { get; set; }
    public DateTime? CreatedAt { get; set; }
}
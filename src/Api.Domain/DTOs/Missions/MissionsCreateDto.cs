namespace Api.Domain.DTOs.Missions;

public class MissionsCreateDto
{
    public string? Goal { get; set; }
    public string? Date { get; set; }
    public string? Duration { get; set; }
    public int? Crew { get; set; }
}
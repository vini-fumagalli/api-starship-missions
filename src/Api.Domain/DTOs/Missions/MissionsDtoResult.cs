namespace Api.Domain.DTOs.Missions;

public class MissionsDtoResult
{
    public string? StarshipName { get; set; }
    public string? Goal { get; set; }
    public string? Date { get; set; }
    public string? Duration { get; set; }
    public int? Crew { get; set; }
    public string? CreatedAt { get; set; }
}
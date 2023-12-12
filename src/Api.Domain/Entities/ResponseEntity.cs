namespace Api.Domain.Entities;

public class ResponseEntity
{
    public bool Success { get; set; }
    public object? Response { get; set; }
}
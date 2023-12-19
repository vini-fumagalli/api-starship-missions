namespace Api.Domain.Entities;

//classe para encapsular todas as respostas obtidas
//pelo Service e enviar até a Controller 
public class ResponseEntity
{
    public bool Success { get; set; }
    public object? Response { get; set; }
}
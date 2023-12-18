using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MissionsController : ControllerBase
{
    //declaração de variável de IMissionsService 
    private readonly IMissionsService _service;

    //inicialização da variável no construtor
    public MissionsController(IMissionsService service)
    {
        _service = service;
    }

    //Todos os métodos da Controller retornam uma ActionResult que
    //encapsula uma ResponseEntity contendo um campo bool Success
    //e um object? Response

    //método POST para cadastrar uma missão.
    //Recebe um objeto da MissionsCreateDto e uma lista de strings com os nomes das 
    //espaçonaves relacionadas à essa missão   
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ResponseEntity>> CreateMission([FromQuery] List<string> starshipNames, MissionsCreateDto mission)
    {
        try
        {
            //verifica se a lista contém algum nome e se a missão não é nula
            //antes de fazer comunicação com a camada de serviço
            if (starshipNames.Count > 0 && mission != null)
            {
                //chama método do Service e aloca resultado na var response
                var response = await _service.CreateMission(starshipNames, mission);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO CADASTRAR MISSÕES => ", ex);
        }
    }

    //método GET para obter as missões relacionadas ao 
    //nome da espaçonave desejada pelo usuário.
    //Esse método também retorna as informações da espaçonave em destaque.
    //Recebe o nome da espaçonave como parâmetro  
    [HttpGet]
    [Route("GetByStarship/{starshipName}")]
    public async Task<ActionResult<ResponseEntity>> GetMissionsByStarship(string starshipName)
    {
        try
        {
            //verifica se o nome é nulo antes de comunicar-se com
            //a camada de serviço
            if(starshipName != null)
            {
                //chama método do Service e aloca o resultado em uma var response 
                var response = await _service.GetMissionsByStarship(starshipName);
                return Ok(response);
            }

            return BadRequest();
            
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER MISSÕES POR ESPAÇONAVE => ", ex);
        }
    }

    //Método GET para obter todas as missões relacionadas à 
    //todas as espaçonaves cadastradas no sistema
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<ResponseEntity>> GetMissions()
    {
        try
        {
            //chama método do Service e aloca o resultado em uma var response
            var response = await _service.GetMissions();
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER TODAS AS MISSÕES => ", ex);
        }
    }




}
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
    private readonly IMissionsService _service;

    public MissionsController(IMissionsService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ResponseEntity>> CreateMission([FromQuery] List<string> starshipNames, MissionsCreateDto mission)
    {
        try
        {
            if (starshipNames.Count > 0)
            {
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

    [HttpGet]
    [Route("GetByStarship/{starshipName}")]
    public async Task<ActionResult<ResponseEntity>> GetMissionsByStarship(string starshipName)
    {
        try
        {
            if(starshipName != null)
            {
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

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<ResponseEntity>> GetMissions()
    {
        try
        {
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
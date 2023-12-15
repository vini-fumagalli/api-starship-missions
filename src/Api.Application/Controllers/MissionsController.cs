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

    [HttpGet]
    [Route("GetByStarship/{starshipName}")]
    public async Task<ActionResult<ResponseEntity>> GetMissionsByStarship(string starshipName)
    {
        try
        {
            var response = await _service.GetMissionsByStarship(starshipName);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER TODAS AS MISSÕES => ", ex);
        }
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ResponseEntity>> CreateMission(MissionsCreateDto mission)
    {
        try
        {
            if (mission.StarshipName != null)
            {
                var response = await _service.CreateMission(mission);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER TODAS AS MISSÕES => ", ex);
        }
    }


}
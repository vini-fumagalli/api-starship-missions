using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class StarshipController : ControllerBase
{
    private readonly IStarshipService _service;

    public StarshipController(IStarshipService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ResponseEntity>> CreateStarship([FromQuery] List<string> models)
    {
        try
        {
            var response = await _service.CreateStarship(models);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO CRIAR ESPAÇONAVE => ", ex);
        }
    }


    [HttpGet]
    [Route("GetByName/{name}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByName(string name)
    {
        try
        {
            var response = await _service.GetStarshipByName(name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE => ", ex);
        }
    }

    [HttpGet]
    [Route("GetByModel/{model}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByModel(string model)
    {
        try
        {
            var response = await _service.GetStarshipByModel(model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE => ", ex);
        }
    }

    [HttpGet]
    [Route("GetByManufacturer/{manufacturer}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByManufacturer(string manufacturer)
    {
        try
        {
            var response = await _service.GetStarshipByManufacturer(manufacturer);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE => ", ex);
        }
    }

    [HttpPut]
    [Route("Update/{name}")]
    public async Task<ActionResult<ResponseEntity>> UpdateStarship(string name, StarshipUpdateDto starship)
    {
        try
        {
            var response = await _service.UpdateStarship(name, starship);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE => ", ex);
        }
    }

    [HttpDelete]
    [Route("Delete/{name}")]
    public async Task<ActionResult<ResponseEntity>> DeleteStarship(string name)
    {
        try
        {
            var response = await _service.DeleteStarship(name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE => ", ex);
        }
    }

}
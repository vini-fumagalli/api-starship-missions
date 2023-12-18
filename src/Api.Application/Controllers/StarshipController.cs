using Api.Domain.DTOs;
using Api.Domain.DTOs.Starship;
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
            if(models.Count > 0)
            {
                var response = await _service.CreateStarship(models);
                return Ok(response);
            }

            return BadRequest();    
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
            if(name != null)
            {
                var response = await _service.GetStarshipByName(name);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR NOME => ", ex);
        }
    }

    [HttpGet]
    [Route("GetByModel/{model}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByModel(string model)
    {
        try
        {
           if(model != null)
            {
                var response = await _service.GetStarshipByModel(model);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR MODELO => ", ex);
        }
    }

    [HttpGet]
    [Route("GetByManufacturer/{manufacturer}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByManufacturer(string manufacturer)
    {
        try
        {
            if(manufacturer != null)
            {
                var response = await _service.GetStarshipByManufacturer(manufacturer);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR FABRICANTE => ", ex);
        }
    }

    [HttpPut]
    [Route("Update/{name}")]
    public async Task<ActionResult<ResponseEntity>> UpdateStarship(string name, StarshipUpdateDto starship)
    {
        try
        {
            if(name != null)
            {
                var response = await _service.UpdateStarship(name, starship);
                return Ok(response);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO ATUALIZAR ESPAÇONAVE => ", ex);
        }
    }

    [HttpDelete]
    [Route("Delete/{name}")]
    public async Task<ActionResult<ResponseEntity>> DeleteStarship(string name)
    {
        try
        {
            if(name != null)
            {
                var response = await _service.DeleteStarship(name);
                return Ok(response);
            }
            
            return BadRequest();
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            throw new Exception("ERRO AO EXCLUIR ESPAÇONAVE => ", ex);
        }
    }

}
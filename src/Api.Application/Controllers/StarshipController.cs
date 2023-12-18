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
    //declaração de variável de IStarshipService
    private readonly IStarshipService _service;

    //inicialização da variável no construtor
    public StarshipController(IStarshipService service)
    {
        _service = service;
    }

    //Todos os métodos da Controller retornam uma ActionResult que
    //encapsula uma ResponseEntity contendo um campo bool Success
    //e um object? Response

    //método POST para cadastrar uma espaçonave.
    //Recebe uma lista de strings com os modelos das 
    //espaçonaves que serão absorvidas da Api SwapiDev
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ResponseEntity>> CreateStarship([FromQuery] List<string> models)
    {
        try
        {
            if(models.Count > 0)
            {
                //verifica se a lista contém algum modelo
                //antes de fazer comunicação com a camada de serviço
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


    //método GET para obter a espaçonave relacionada ao 
    //nome da espaçonave desejada pelo usuário.
    //Recebe o nome da espaçonave como parâmetro  
    [HttpGet]
    [Route("GetByName/{name}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByName(string name)
    {
        try
        {
            //verifica se o nome é nulo antes de comunicar-se com
            //a camada de serviço
            if(name != null)
            {
                //chama método do Service e aloca o resultado em uma var response
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


    //método GET para obter a espaçonave relacionada ao 
    //modelo da espaçonave desejada pelo usuário.
    //Recebe o modelo da espaçonave como parâmetro
    [HttpGet]
    [Route("GetByModel/{model}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByModel(string model)
    {
        try
        {
            //verifica se o modelo é nulo antes de comunicar-se com
            //a camada de serviço
           if(model != null)
            {
                //chama método do Service e aloca o resultado em uma var response
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


    //método GET para obter a espaçonave relacionada ao 
    //fabricante da espaçonave desejada pelo usuário.
    //Recebe o fabricante da espaçonave como parâmetro
    [HttpGet]
    [Route("GetByManufacturer/{manufacturer}")]
    public async Task<ActionResult<ResponseEntity>> GetStarshipByManufacturer(string manufacturer)
    {
        try
        {
            //verifica se o fabricante é nulo antes de comunicar-se com
            //a camada de serviço
            if(manufacturer != null)
            {
                //chama método do Service e aloca o resultado em uma var response
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


    //método PUT para atualizar a espaçonave desejada pelo usuário
    //Recebe o nome da espaçonave e um objeto de StarshipUpdateDto
    //como parâmetro
    [HttpPut]
    [Route("Update/{name}")]
    public async Task<ActionResult<ResponseEntity>> UpdateStarship(string name, StarshipUpdateDto starship)
    {
        try
        {
            //verifica se o nome é nulo antes de comunicar-se com
            //a camada de serviço
            if(name != null)
            {
                //chama método do Service e aloca o resultado em uma var response
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


    //método DELETE para excluir a espaçonave desejada pelo usuário
    //Recebe o nome da espaçonave como parâmetro
    [HttpDelete]
    [Route("Delete/{name}")]
    public async Task<ActionResult<ResponseEntity>> DeleteStarship(string name)
    {
        try
        {
            //verifica se o nome é nulo antes de comunicar-se com
            //a camada de serviço
            if(name != null)
            {
                //chama método do Service e aloca o resultado em uma var response
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
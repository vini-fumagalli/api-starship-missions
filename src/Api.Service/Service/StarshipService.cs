using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class StarshipService : IStarshipService
{
    private readonly IStarshipAndMissionsRepository _repository;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public StarshipService(IStarshipAndMissionsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _httpClient = new HttpClient();

    }
    public async Task<ResponseEntity> CreateStarship(List<string> starshipModels)
    {
        try
        {
            var starshipList = new List<StarshipEntity>();

            foreach (var model in starshipModels)
            {
                if (model != null)
                {
                    string apiUrl = "https://swapi.dev/api/starships/?search=" + Uri.EscapeDataString(model);
                    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiUrl);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                        var searchResponse = JsonSerializer.Deserialize<StarshipSearchResponseDto>(jsonResponse);

                        if(searchResponse!.Results!.Count > 0)
                        {
                            var starshipCreateDto = searchResponse!.Results![0];
                            var starshipEntity = _mapper.Map<StarshipEntity>(starshipCreateDto);
                            starshipList.Add(starshipEntity);
                        }
                        
                    }
                }
            }
            var response = await _repository.CreateStarship(starshipList);
            var formattedResponse = _mapper.Map<List<StarshipDtoResult>>(response);
            
            return new ResponseEntity
            {
                Success = formattedResponse.Any(),
                Response = formattedResponse
            };
        }
        catch(Exception ex)
        {
            throw new Exception("ERRO AO CRIAR ESPAÇONAVES => ", ex);
        }

    }

    public async Task<ResponseEntity> DeleteStarship(string name)
    {
        name = name.Replace(" ", ".").ToUpper();
        var response = await _repository.DeleteStarship(name);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetStarshipByManufacturer(string manufacturer)
    {
        manufacturer = manufacturer.Replace(" ", ".").ToUpper();
        var starship = await _repository.GetStarshipByManufacturer(manufacturer);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetStarshipByModel(string model)
    {
        model = model.Replace(" ", ".").ToUpper();
        var starship = await _repository.GetStarshipByModel(model);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetStarshipByName(string name)
    {
        name = name.Replace(" ", ".").ToUpper();
        var starship = await _repository.GetStarshipByName(name);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> UpdateStarship(string name, StarshipUpdateDto starship)
    {

        var starshipToUpdate = _mapper.Map<StarshipEntity>(starship);
        starshipToUpdate.Name = name.Replace(" ", ".").ToUpper();

        var starshipUpdated = await _repository.UpdateStarship(starshipToUpdate);
        var response = _mapper.Map<StarshipDtoResult>(starshipUpdated);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }
}
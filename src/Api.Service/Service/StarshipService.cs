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
    public async Task<ResponseEntity> Create(List<string> starshipModels)
    {
        var starshipList = new List<StarshipEntity>();

        foreach (var model in starshipModels)
        {
            if (model != null)
            {
                string apiUrl = "https://swapi.dev/api/starships/?search=" + model;
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    var starship = JsonSerializer.Deserialize<StarshipEntity>(jsonResponse)!;
                    starshipList.Add(starship);
                }
            }
        }

        if (starshipList.Count > 0)
        {
            var response = await _repository.CreateStarship(starshipList);
            return new ResponseEntity
            {
                Success = true,
                Response = response
            };
        }

        return new ResponseEntity
        {
            Success = false,
            Response = null
        };

    }

    public async Task<ResponseEntity> Delete(string name)
    {
        var response = await _repository.DeleteStarship(name);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetByManufacturer(string manufacturer)
    {
        var starship = await _repository.GetStarshipByManufacturer(manufacturer);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetByModel(string model)
    {
        var starship = await _repository.GetStarshipByModel(model);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> GetByName(string name)
    {
        var starship = await _repository.GetStarshipByName(name);
        var response = _mapper.Map<StarshipDtoResult>(starship);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    public async Task<ResponseEntity> Update(string name, StarshipUpdateDto starship)
    {

        var starshipToUpdate = _mapper.Map<StarshipEntity>(starship);
        starshipToUpdate.Name = name;

        var starshipUpdated = await _repository.UpdateStarship(starshipToUpdate);
        var response = _mapper.Map<StarshipDtoResult>(starshipUpdated);

        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }
}
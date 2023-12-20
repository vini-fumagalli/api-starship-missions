using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Domain.DTOs;
using Api.Domain.DTOs.Starship;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

//Camada de Service destinada a implementar as regras de 
//negócio da aplicação antes de chamar o Repository
public class StarshipService : IStarshipService
{
    //declaro uma variável de HttpClient para
    //solicitar a incorporação de outra Api na minha e
    //declaro como estática em prol de reutilização
    private static readonly HttpClient _httpClient = new HttpClient();
    //declaro uma variável de IStarshipAndMissionsRepository para
    //acessar os métodos do Repositório e uma de IMapper para utilizar o
    //Mapping
    private readonly IStarshipAndMissionsRepository _repository;
    private readonly IMapper _mapper;

    //inicializo as variáveis dinâmicas no construtor
    public StarshipService(IStarshipAndMissionsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    //Método para criar uma ou mais espaçonaves que 
    //recebe uma lista com os modelos das espaçonaves 
    public async Task<ResponseEntity> CreateStarship(List<string> starshipModels)
    {
        try
        {
            //instancio uma lista de StarshipEntity antes do loop
            var starshipList = new List<StarshipEntity>();

            //loop para interagir com cada modelo da lista
            foreach (var model in starshipModels)
            {
                //verifico se o modelo da vez não é null
                if (model != null)
                {
                    //defino uma urlbase para a busca das infos de cada espaçonave com
                    //base no model analisado pelo loop  
                    var apiUrl = "https://swapi.dev/api/starships/?search=" + Uri.EscapeDataString(model);
                    //uso GetAsync() para fazer uma requisição naquela Uri
                    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiUrl);
                    //verifico se obtive sucesso
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        //leio o JSON que contém uma lista de results obtido na requisição como string e
                        //e aloco em uma variável
                        var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                        //Deserializo a string de JSON em um no atributo Results de 
                        //um objeto de StarshipSearchResponseDto para colocar cada atributo do JSON 
                        //em seu respectivo destino na classe StarshipCreateDto   
                        var searchResponse = JsonSerializer.Deserialize<StarshipSearchResponseDto>(jsonResponse);

                        //searchResponse é uma lista, portanto verifico
                        //se há elementos antes de prosseguir 
                        if (searchResponse!.Results!.Count > 0)
                        {
                            //acesso sempre o primeiro item da lista
                            //pois a cada loop uma nova lista com um elemento será
                            //instanciada para q a deserialização de JSON funcione 
                            var starshipCreateDto = searchResponse!.Results![0];
                            //faço mapping para entity e adiciono na lista criada
                            //no começo
                            var starshipEntity = _mapper.Map<StarshipEntity>(starshipCreateDto);
                            starshipList.Add(starshipEntity);
                        }

                    }
                }
            }
            //mando a lista para o repositório
            var response = await _repository.CreateStarship(starshipList);
            //faço mapping para DtoResult e envio uma RespostaEntity 
            //para a controller
            var formattedResponse = _mapper.Map<List<StarshipDtoResult>>(response);

            return new ResponseEntity
            {
                Success = formattedResponse.Any(),
                Response = formattedResponse
            };
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO CRIAR ESPAÇONAVES => ", ex);
        }

    }

    //método para excluir espaçonave desejada pelo 
    //usuário através do nome dela
    public async Task<ResponseEntity> DeleteStarship(string name)
    {
        //formato o nome para o padrão armazenado 
        //no banco de dados e envio ao repositório 
        name = name.Replace(" ", ".").ToUpper();
        var response = await _repository.DeleteStarship(name);

        //por fim, devolvo uma RespostaEntity
        //para a controller
        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    //método para obter uma espaçonave por fabricante
    public async Task<ResponseEntity> GetStarshipByManufacturer(string manufacturer)
    {
        //formato o fabricante para o padrão alocado no BD 
        manufacturer = manufacturer.Replace(" ", ".").ToUpper();
        //chamo o repositório e faço mapping com o
        //objeto obtido
        var starship = await _repository.GetStarshipByManufacturer(manufacturer);
        var response = _mapper.Map<StarshipDtoResult>(starship);
        //por fim, devolvo uma RespostaEntity
        //para a controller
        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    //método para obter uma espaçonave por modelo
    public async Task<ResponseEntity> GetStarshipByModel(string model)
    {
        //formato o modelo para o padrão alocado no BD 
        model = model.Replace(" ", ".").ToUpper();
        //chamo o repositório e faço mapping com o
        //objeto obtido
        var starship = await _repository.GetStarshipByModel(model);
        var response = _mapper.Map<StarshipDtoResult>(starship);
        //por fim, devolvo uma RespostaEntity
        //para a controller
        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    //método para obter uma espaçonave por nome
    public async Task<ResponseEntity> GetStarshipByName(string name)
    {
        //formato o nome para o padrão alocado no BD 
        name = name.Replace(" ", ".").ToUpper();
        //chamo o repositório e faço mapping com o
        //objeto obtido
        var starship = await _repository.GetStarshipByName(name);
        var response = _mapper.Map<StarshipDtoResult>(starship);
        //por fim, devolvo uma RespostaEntity
        //para a controller
        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }

    //método para atualizar espaçonave que recebe o nome
    //da espaçonave e o restante das atualizações em forma de 
    //objeto de StarshipUpdateDto
    public async Task<ResponseEntity> UpdateStarship(string name, StarshipUpdateDto starship)
    {
        //faço mapping para entity
        var starshipToUpdate = _mapper.Map<StarshipEntity>(starship);
        //aloco o nome formatado explicitamente pois esse
        //atributo não pertence a StarshipUpdateDto
        starshipToUpdate.Name = name.Replace(" ", ".").ToUpper();
        //chamo o repositório e faço mapping com a resposta obtida
        var starshipUpdated = await _repository.UpdateStarship(starshipToUpdate);
        var response = _mapper.Map<StarshipDtoResult>(starshipUpdated);
        //por fim, devolvo uma RespostaEntity
        //para a controller
        return new ResponseEntity
        {
            Success = true,
            Response = response
        };
    }
}
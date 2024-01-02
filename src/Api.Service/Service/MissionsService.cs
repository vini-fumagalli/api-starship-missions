using Api.Domain.DTOs;
using Api.Domain.DTOs.Missions;
using Api.Domain.DTOs.Starship;
using Api.Domain.DTOs.StarshipMissions;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

//Camada de Service destinada a implementar as regras de 
//negócio da aplicação antes de chamar o Repository
public class MissionsService : IMissionsService
{
    //declara variável do tipo da interface de repositório
    //para acessá-lo através de injeção de dependência
    //e declara também variável de IMapper para fazer Mapping 
    private readonly IStarshipAndMissionsRepository _repository;
    private readonly IMapper _mapper;

    public MissionsService(IStarshipAndMissionsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    //Método para criar uma ou mais missões 
    //recebe a lista de nomes das espaçonaves relacionadas à missão
    //e a missão propriamente
    public async Task<ResponseEntity> CreateMission(List<string> starshipNames, MissionsCreateDto mission)
    {
        //instancia lista antes do loop
        var missionsList = new List<MissionsEnitity>();

        //loop para interagir com cada nome de espaçonave na lista
        foreach (var starshipName in starshipNames)
        {
            //edita nome para padronizar no banco de dados com esse padrão
            var formattedStarshipName = starshipName.Replace(" ", ".").ToUpper();
            //verifica se existe, já que só se pode cadastrar uma espaçonave em uma
            //missão se essa espaçonave ja estiver no sistema
            var existsStarship = await _repository.GetStarshipByName(formattedStarshipName);
            if (existsStarship != null)
            {
                //faz mapping de DTO para Entity
                var missionsEntity = _mapper.Map<MissionsEnitity>(mission);
                //adciona o StarshipName explicitamente pois a DTO 
                //não possui esse atributo
                missionsEntity.StarshipName = formattedStarshipName;
                //por fim, adiciona na lista
                missionsList.Add(missionsEntity);
            }
        }

        //após o loop, chama o repositório
        var response = await _repository.CreateMission(missionsList);
        //formata a resposta 
        var formattedResponse = _mapper.Map<List<MissionsDtoResult>>(response);
        //devolve uma ResponseEntity para a Controller
        return new ResponseEntity
        {
            //uso Any() pois se não houver elementos na lista
            //o Success será false
            Success = formattedResponse.Any(),
            Response = formattedResponse
        };
    }

    //método para obter todas as missões 
    //de todas as espaçonaves cadastradas 
    public async Task<ResponseEntity> GetMissions()
    {
        //puxa todas as espaçonaves cadastradas
        var starshipList = await _repository.GetStarships();
        //instancia uma lista de StarshipMissionsDtoResult antes do loop
        var starshipMissionsList = new List<StarshipMissionsDtoResult>();

        //loop para interagir com cada espaçonave da starshipList
        foreach (var starship in starshipList)
        {
            //através do nome da espaçonave analisada
            //chama o método GetMissionsByStarship e registra todas as missões 
            //dessa espaçonave em um objeto
            var starshipMissionsDto = await _repository.GetMissionsByStarship(starship.Name!);
            //fatora a starship obtida e a lista de missões com o
            //método que chama o Mapping
            var missionsByStarship = MapToStarshipMissionsDtoResult(starshipMissionsDto.Starship!,
                                                                    starshipMissionsDto.Missions!);
            //adiciona na lista
            starshipMissionsList.Add(missionsByStarship);
        }
        //devolve uma ResponseEntity para a Controller
        return new ResponseEntity
        {
            //uso Any() pois se não houver elementos na lista
            //o Success será false
            Success = starshipMissionsList.Any(),
            Response = starshipMissionsList
        };
    }

    //método para obter todas as missões de uma espaçonave 
    //específica desejada pelo usuário através do nome da 
    //espaçonave
    public async Task<ResponseEntity> GetMissionsByStarship(string starshipName)
    {
        //edita nome para padronizar no banco de dados com esse padrão
        starshipName = starshipName.Replace(" ", ".").ToUpper();
        //obtém um objeto q contém a info da starship 
        //e a lista de missões nas quais ela esteve presente
        var starshipMissionsDto = await _repository.GetMissionsByStarship(starshipName);
        //chama método para fazer Mapping
        var response = MapToStarshipMissionsDtoResult(starshipMissionsDto.Starship!,
                                                      starshipMissionsDto.Missions!);

        //devolve uma ResponseEntity para a Controller
        return new ResponseEntity
        {
            Success = response != null,
            Response = response
        };
    }

    //esse método privado foi criado para tornar o código mais legível
    //já que os métodos GetMissionsByStarship e GetMissions usariam
    //as mesmas linhas de código no corpo do método abaixo em prol de
    //realizar o Mapping
    private StarshipMissionsDtoResult MapToStarshipMissionsDtoResult
    (StarshipEntity starship, List<MissionsEnitity> missions)
    {
        var formattedStarship = _mapper.Map<StarshipDtoResult>(starship);
        var formattedMissions = _mapper.Map<List<MissionsDtoResult>>(missions);

        return new StarshipMissionsDtoResult
        {
            Starship = formattedStarship,
            Missions = formattedMissions
        };
    }
}
using Api.Data.Context;
using Api.Domain.DTOs;
using Api.Domain.DTOs.StarshipMissions;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

//camada de Data contém o Repositório: responsável
//por estabelecer a comunicação com BD através do
//Entity Framework
public class StarshipAndMissionsRepository : IStarshipAndMissionsRepository
{
    //declaro um variável do MyContext para ter um acesso
    //geral ao BD e duas DbSet que interagem com as tabelas de
    //Missões e Espaçonaves
    private readonly MyContext _context;
    private readonly DbSet<StarshipEntity> _dbStarship;
    private readonly DbSet<MissionsEnitity> _dbMissions;

    //inicializo as variáveis no construtor
    public StarshipAndMissionsRepository(MyContext context)
    {
        _context = context;
        _dbStarship = _context.Set<StarshipEntity>();
        _dbMissions = _context.Set<MissionsEnitity>();
    }

    //o uso de AsNoTracking() em alguns métodos abaixo
    //tem a finalidade de desativar o rastreamento do Entity
    //Framework já que, em situações de exibição de dados nas quais
    //nenhuma alteração é feita, o rastreamento é desnecessário

    //Método para criar uma ou mais espaçonaves
    //que recebe uma lista de StashipEntity
    public async Task<List<StarshipEntity>> CreateStarship(List<StarshipEntity> starships)
    {
        try
        {
            //loop para verificar a existência de cada
            //espaçonave no BD e, posteriormente, adicioná-la 
            //ou não 
            foreach (var starship in starships)
            {
                var exists = await _dbStarship.AnyAsync(s => s.Name!.Equals(starship.Name));
                if (!exists)
                {
                    await _dbStarship.AddAsync(starship);
                }
            }
            //após o loop, salva as alterações e 
            //retorna a(s) espaçonave(s) 
            await _context.SaveChangesAsync();
            return starships;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO CADASTRAR ESPAÇONAVE => ", ex);
        }
    }

    //Método para excluir uma espaçonave do BD
    //que recebe o nome da espaçonave em questão
    public async Task<bool> DeleteStarship(string name)
    {
        try
        {
            //verifica se espaçonave está no BD. Se sim,
            //faz o delete no banco de dados e retorna true.
            //Caso contrário, retorna false
            var starship = await _dbStarship.SingleOrDefaultAsync(s => s.Name!.Equals(name));
            if (starship != null)
            {
                _dbStarship.Remove(starship);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO EXCLUIR ESPAÇONAVE => ", ex);
        }
    }

    //Método para obter as missões relacionadas à espaçonave 
    //desejada pelo usuário
    public async Task<StarshipMissionsDto> GetMissionsByStarship(string starshipName)
    {
        try
        {
            //obtém as missões que contém o nome da espaçonave 
            var missions = await _dbMissions.Where(m => m.StarshipName!.Equals(starshipName)).ToListAsync();
            //obtém as informações da espaçonave 
            var starship = await _dbStarship.SingleOrDefaultAsync(s => s.Name!.Equals(starshipName));
            //instancia um objeto de StarshipMissionsDto para retornar
            //dados obtidos
            var starshipMissions = new StarshipMissionsDto
            {
                Starship = starship,
                Missions = missions
            };

            return starshipMissions;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER AS MISSÕES POR ESPAÇONAVE => ", ex);
        }
    }

    //Método para criar uma ou mais missões que recebe 
    //uma lista de MissionsEntity
    public async Task<List<MissionsEnitity>> CreateMission(List<MissionsEnitity> missions)
    {
        try
        {
            //loop para adicionar cada missão
            //da lista no BD
            foreach(var mission in missions)
            {
                await _dbMissions.AddAsync(mission);    
            }
            //salva as alterações e retorna as missões
             await _context.SaveChangesAsync();
             return missions;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO CADASTRAR MISSÃO => ", ex);
        }
    }

    //Método para obter as espaçonave pelo 
    //fabricante desejado pelo usuário
    public async Task<StarshipEntity?> GetStarshipByManufacturer(string manufacturer)
    {
        try
        {
            //obtém a espaçonave pelo fabricante e a retorna  
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Manufacturer!.Equals(manufacturer));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR FABRICANTE => ", ex);
        }
    }

    //Método para obter as espaçonave pelo 
    //modelo desejado pelo usuário
    public async Task<StarshipEntity?> GetStarshipByModel(string model)
    {
        try
        {
            //obtém a espaçonave pelo modelo e a retorna
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Model!.Equals(model));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR MODELO => ", ex);
        }
    }

    //Método para obter as espaçonave pelo 
    //nome desejado pelo usuário
    public async Task<StarshipEntity?> GetStarshipByName(string name)
    {
        try
        {
            //obtém a espaçonave pelo nome e a retorna
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Name!.Equals(name));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR NOME => ", ex);
        }
    }

    //Método para obter todas as espaçonaves 
    //registradas no sistema
    public async Task<List<StarshipEntity>> GetStarships()
    {
        try
        {
            return await _dbStarship.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVES => ", ex);
        }
    }

    //Método para atualizar os dados de uma espaçonave 
    //informada pelo usuário
    public async Task<StarshipEntity?> UpdateStarship(StarshipEntity starship)
    {
        try
        {
            //obtém a espaçonave e verifica se ela existe
            var starshipToUpdate = await _dbStarship
                                        .SingleOrDefaultAsync(s => s.Name!.Equals(starship.Name));

            if (starshipToUpdate != null)
            {
                //pega o dado de CreatedAt dos dados antigos da espaçonave
                //e coloca nos novos
                starship.CreatedAt = starshipToUpdate.CreatedAt;

                //acessa os dados anteriores da espaçonave
                //e substitui pelos novos
                _dbStarship.Entry(starshipToUpdate).CurrentValues.SetValues(starship);
                await _context.SaveChangesAsync();
                return starship;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO ATUALIZAR ESPAÇONAVE => ", ex);
        }


    }
}
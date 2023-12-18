using Api.Data.Context;
using Api.Domain.DTOs;
using Api.Domain.DTOs.StarshipMissions;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class StarshipAndMissionsRepository : IStarshipAndMissionsRepository
{
    private readonly MyContext _context;
    private readonly DbSet<StarshipEntity> _dbStarship;
    private readonly DbSet<MissionsEnitity> _dbMissions;

    public StarshipAndMissionsRepository(MyContext context)
    {
        _context = context;
        _dbStarship = _context.Set<StarshipEntity>();
        _dbMissions = _context.Set<MissionsEnitity>();
    }

    public async Task<List<StarshipEntity>> CreateStarship(List<StarshipEntity> starships)
    {
        try
        {
            foreach (var starship in starships)
            {
                var exists = await _dbStarship.AnyAsync(s => s.Name!.Equals(starship.Name));
                if (!exists)
                {
                    await _dbStarship.AddAsync(starship);
                }
            }
            await _context.SaveChangesAsync();
            return starships;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO CADASTRAR ESPAÇONAVE => ", ex);
        }
    }

    public async Task<bool> DeleteStarship(string name)
    {
        try
        {
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

    public async Task<StarshipMissionsDto> GetMissionsByStarship(string starshipName)
    {
        try
        {
            var missions = await _dbMissions.Where(m => m.StarshipName!.Equals(starshipName)).ToListAsync();
            var starship = await _dbStarship.SingleOrDefaultAsync(s => s.Name!.Equals(starshipName));
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

    public async Task<List<MissionsEnitity>> CreateMission(List<MissionsEnitity> missions)
    {
        try
        {
            foreach(var mission in missions)
            {
                await _dbMissions.AddAsync(mission);    
            }
             await _context.SaveChangesAsync();
             return missions;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO CADASTRAR MISSÃO => ", ex);
        }
    }

    public async Task<StarshipEntity?> GetStarshipByManufacturer(string manufacturer)
    {
        try
        {
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Manufacturer!.Equals(manufacturer));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR FABRICANTE => ", ex);
        }
    }

    public async Task<StarshipEntity?> GetStarshipByModel(string model)
    {
        try
        {
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Model!.Equals(model));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR MODELO => ", ex);
        }
    }

    public async Task<StarshipEntity?> GetStarshipByName(string name)
    {
        try
        {
            var starship = await _dbStarship.AsNoTracking()
                                            .SingleOrDefaultAsync(s => s.Name!.Equals(name));
            return starship;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER ESPAÇONAVE POR NOME => ", ex);
        }
    }

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

    public async Task<StarshipEntity?> UpdateStarship(StarshipEntity starship)
    {
        try
        {
            var starshipToUpdate = await _dbStarship
                                        .SingleOrDefaultAsync(s => s.Name!.Equals(starship.Name));

            if (starshipToUpdate != null)
            {
                starship.CreatedAt = starshipToUpdate.CreatedAt;

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
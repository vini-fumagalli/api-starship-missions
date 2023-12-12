using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.CrossCutting.DependencyInjection;

public class ConfigureRepository
{
    public static void ConfigureDependenciesRepository(IServiceCollection service, string chave)
    {
        service.AddScoped(typeof(IStarshipAndMissionsRepository), typeof(StarshipAndMissionsRepository));

        service.AddDbContextFactory<MyContext>(
        options => options.UseSqlServer(Environment.GetEnvironmentVariable(chave, EnvironmentVariableTarget.Machine))
        );
    }
}
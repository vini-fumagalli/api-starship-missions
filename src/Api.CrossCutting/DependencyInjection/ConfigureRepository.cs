using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.CrossCutting.DependencyInjection;

public class ConfigureRepository
{
    //método para configurar injeção de dependência do Repository
    public static void ConfigureDependenciesRepository(IServiceCollection service, string chave)
    {
        //toda vez q for solicitado uma instância de IStarshipAndMissionsRepository 
        //o programa devolverá uma instância de StarshipAndMissionsRepository dentro do escopo da requisição 
        service.AddScoped(typeof(IStarshipAndMissionsRepository), typeof(StarshipAndMissionsRepository));

        //adiciona um contexto de banco de dados (DbContext) à fábrica de contexto
        //obtém a connectionString através de variável de ambiente para conferir camuflagem ao BD
        service.AddDbContextFactory<MyContext>(
        options => options.UseSqlServer(Environment.GetEnvironmentVariable(chave, EnvironmentVariableTarget.Machine))
        );
    }
}
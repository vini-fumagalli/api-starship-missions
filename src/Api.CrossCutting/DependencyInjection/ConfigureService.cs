using Api.Domain.Interfaces.Services;
using Api.Service.Services;

namespace Api.CrossCutting.DependencyInjection;

public class ConfigureService
{
    //método para configurar injeção de dependência dos Services
    public static void ConfigureDependenciesService(IServiceCollection service)
    {
        //toda vez q uma instância da Interface de serviço for solicitada
        //o programa devolverá uma nova instância do Serviço em si em diferentes
        //operações dentro da mesma requisição  
        service.AddTransient<IStarshipService, StarshipService>();
        service.AddTransient<IMissionsService, MissionsService>();
    }
}
using Api.Domain.Interfaces.Services;
using Api.Service.Services;

namespace Api.CrossCutting.DependencyInjection;

public class ConfigureService
{
    public static void ConfigureDependenciesService(IServiceCollection service)
    {
        service.AddTransient<IStarshipService, StarshipService>();
        service.AddTransient<IMissionsService, MissionsService>();
    }
}
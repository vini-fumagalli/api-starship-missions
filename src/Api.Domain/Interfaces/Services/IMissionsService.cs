using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IMissionsService
{
    Task<ResponseEntity> GetMissions();
}
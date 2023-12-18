using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mapping;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Configuração de Mapping
builder.Services.AddAutoMapper(typeof(DtoToEntityProfile));
builder.Services.AddAutoMapper(typeof(EntityToDtoResultProfile));

//Configuração de Injeção de Dependência
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, "DB_CONNECTION_STARSHIP");
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Adição de informações do autor
builder.Services.AddSwaggerGen(s =>
        s.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Menu de Missões e Espaçonaves",
            Description = "=> Api de cadastro de Espaçonaves e Missões construída com arquitetura DDD que consome outra Api (SwapiDev): " +
            "https://swapi.dev/ \n\n => Banco de Dados feito através de migrações pelo EntityFramework",
            TermsOfService = new Uri("http://www.linkedin.com/in/vini-fumagalli"),
            Contact = new OpenApiContact
            {
                Name = "Vinícius Fumagalli",
                Email = "vinifumagalli_@hotmail.com",
            },
            License = new OpenApiLicense
            {
                Name = "Termo de Licença de Uso",
                Url = new Uri("http://www.linkedin.com/in/vini-fumagalli")
            }
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Starships and Missions");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

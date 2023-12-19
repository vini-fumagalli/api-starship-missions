using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context;

//classe que herda de IDesignTimeDbContextFactory<MyContext>
//indicando que instâncias do MyConetxt serão criadas em tempo de design para Migrations
public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    //connectionString é obtida através de váriavel de ambiente 
    //para conferir camuflagem ao BD
    private readonly string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STARSHIP", EnvironmentVariableTarget.Machine)!;

    //método para criar a instância de DbContextOptionsBuilder
    //para configurar opções do MyContext 
    public MyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

        //optionsBuilder ativa o carregamento tardio por proxy
        //para q os dados sejam utilizados apenas quando realmente necessários
        //e define o SQLServer como provedor para o MyContext
        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
        return new MyContext(optionsBuilder.Options);
    }
}
using ContactBook.Application.Interfaces;
using ContactBook.Infrastructure.Persistence;
using ContactBook.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the AppDbContext with the connection string from configuration
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register repositories, services, etc. here
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}

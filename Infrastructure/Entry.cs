using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Entry
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
        services.AddScoped<IRepository<Cart>, CartRepository>();
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<Customer>, CustomerRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<UnitOfWork>();
        return services;
    }
}
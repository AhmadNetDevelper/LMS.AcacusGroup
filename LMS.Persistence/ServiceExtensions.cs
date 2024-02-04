using LMS.Application.Repositories;
using LMS.Domain;
using LMS.Domain.Entities;
using LMS.Persistence.Context;
using LMS.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace LMS.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        IdentityBuilder builder = services.AddDependencyInjectionCore(configuration);

        builder.AddEntityFrameworkStores<DataContext>();
        services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SqlDB")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<ICheckoutRepository, CheckoutRepository>();
        services.AddScoped<IPatronRepository, PatronRepository>();
    }
}
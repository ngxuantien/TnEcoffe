using Microsoft.Extensions.DependencyInjection;
using TnEcoffe.Application.Interfaces;
using TnEcoffe.Application.Services;

namespace TnEcoffe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISettingsService, SettingsService>();

        return services;
    }
}
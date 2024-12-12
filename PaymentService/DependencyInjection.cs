using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.CibPayIntegration.Implementations;

namespace PaymentService;

public static class DependencyInjection
{
    public static IServiceCollection AddCibPayServiceIntegration(this IServiceCollection services)
    {
        services.AddScoped<CibPayService>();
        services.AddHttpClient();

        return services;
    }
}
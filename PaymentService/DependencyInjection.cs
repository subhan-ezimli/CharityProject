using C.Service.Payment.CibPay.Implementations;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.CibPayIntegration.Abstractions;
//using PaymentService.CibPayIntegration.Implementations;

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
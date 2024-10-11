namespace WebApp.Infrastructure
{
    internal static class CorsUtils
    {

        internal const string PolicyName = "AllowCors";

        internal static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, builder => builder
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin()
                     .SetIsOriginAllowed(o => true)
                     .Build());
            });
            return services;
        }
    }
}

﻿using B.Repository.Common;
using D.Dal.SqlServer.Context;
using D.Dal.SqlServer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace D.Dal.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, SqlUnitOfWork>(provider =>
        {
            var dbContext = provider.GetRequiredService<AppDbContext>();
            return new SqlUnitOfWork(dbContext);
        }
            );

        return services;
    }
}

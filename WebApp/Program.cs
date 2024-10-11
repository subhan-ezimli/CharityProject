using B.Repository.Common;
using D.Dal.SqlServer;
using D.Dal.SqlServer.Context;
using D.Dal.SqlServer.UnitOfWork;
using E.Application;
using E.Application.Security;
using Microsoft.AspNetCore.Cors.Infrastructure;
using WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddScoped<IUnitOfWork, SqlUnitOfWork>((provider) =>
//{
//    var dbContext = provider.GetRequiredService<AppDbContext>();
//    return new SqlUnitOfWork(dbContext);
//});

//app.UseHttpsRedirection();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSqlServerServices(connectionString);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUSerContext, HttpUserContext>();

builder.Services.AddScoped<IUnitOfWork, SqlUnitOfWork>(provider =>
{
    var dbContext = provider.GetRequiredService<AppDbContext>();
    return new SqlUnitOfWork(dbContext);
}
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorsMiddleware>();
app.UseCors();

app.UseAuthorization();


app.MapControllers();

app.Run();
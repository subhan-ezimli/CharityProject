using D.Dal.SqlServer;
using E.Application;
using E.Application.Security;
using WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);




var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSqlServerServices(connectionString);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(x => x.AddPolicy("CorsPolicy", builder =>
{
    builder
    //.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed(x => true)
    .Build();
}));


builder.Services.AddScoped<IUSerContext, HttpUserContext>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("CorsPolicy");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CorsMiddleware>();
//app.UseCors();

app.UseAuthorization();


app.MapControllers();

app.Run();
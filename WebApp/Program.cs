using D.Dal.SqlServer;
using E.Application;
using E.Application.Security;
using PaymentService;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApp.Extensions;
using WebApp.Infrastructure;
using WebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSqlServerServices(connectionString);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddCors(x => x.AddPolicy("CorsPolicy", builder =>
//{
//    builder
//    .AllowAnyOrigin()
//    //.WithOrigins("http://localhost:4200")
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    //.AllowCredentials()
//    //.SetIsOriginAllowed(x => true)
//    .Build();
//}));


builder.Services.AddScoped<IUserContext, HttpUserContext>();

builder.Services.AddCibPayServiceIntegration();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthenticationDependency(builder.Configuration);


builder.Services.AddSwaggerGen();


builder.Services.AddControllers()
   .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
   .AddJsonOptions(options =>
   {
       //options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
       options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
       //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

   });

var app = builder.Build();

//app.UseCors("CorsPolicy");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors();
app.UseMiddleware<CorsMiddleware>();
//app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


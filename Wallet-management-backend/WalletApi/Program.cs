global using WalletApi.Data;
global using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WalletApi.Authorization;
using WalletApi.Helpers;
using WalletApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("defaultConn")
    );
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );

    x.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings")
);

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddHttpClient();

builder.Services.AddCors(p =>
    p.AddPolicy("corspolicy", build =>
    {
        build.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    })
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corspolicy");

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

var port =
    Environment.GetEnvironmentVariable("PORT")
    ?? "8080";

app.Run($"http://0.0.0.0:{port}");

using Microsoft.AspNetCore.HttpOverrides;
using BizLogic;
using BizLogic.concrete;
using Persistence;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using Persistence.Repository;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.ClientServices;
using Services.ClientServices.Concrete;
using Services.AdminServices;
using Services.AdminServices.Concrete;
using PizzaWebAPI.Extensions;
using NLog;
using Services.LoggerService;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();


ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "PizzaWebSite",
        ValidAudience = "AudiencePizzaWebSite",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Persistence"))); 

builder.Services.AddScoped<IClientUseCase, ClientUseCase>();
builder.Services.AddScoped<IPizzaUseCase, PizzaUseCase>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IPageListOrdersService, PageListOrdersService>();
builder.Services.AddScoped<IPageListPizzaService, PageListPizzaService>();
builder.Services.AddScoped<IEditCustomerService, EditCustomerService>();
builder.Services.AddScoped<IEditPizzaService, EditPizzaService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
//From UltimateASPNETCore

//var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExceptionHandler(logger);

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
//=======================

app.MapControllers();

app.Run();

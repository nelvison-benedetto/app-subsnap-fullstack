using FluentValidation;
using SubSnap.API.StartupExtensions.Authentication;
using SubSnap.API.StartupExtensions.Authorization;
using SubSnap.API.StartupExtensions.Cors;
using SubSnap.API.StartupExtensions.HealthChecks;
using SubSnap.API.StartupExtensions.Swagger;
using SubSnap.API.StartupExtensions.Validation;

//using Microsoft.OpenApi.Models;
using SubSnap.API.Validators;
using SubSnap.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Configurations
builder.Services.AddValidationConfiguration();
builder.Services.AddCorsConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationConfiguration();
builder.Services.AddHealthChecksConfiguration();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

// AutoMapper (FIX AMBIGUITÀ)
builder.Services.AddAutoMapper(typeof(Program).Assembly);  //tieni solo plugin Automapper e togli plugin AutomapperExtension.ect xk seno c'è ambiguita x c# se usi 'AddAutoMapper'

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
    //qua accade DbContext, repositories concreti, unit of work, connection str

var app = builder.Build();  //crei l'istanza finale dell'app. ora elenchi i middlewares (http chain)

// Middleware pipeline
app.UseSwaggerConfiguration();
app.UseCorsConfiguration();
app.UseHttpsRedirection();
app.UseAuthentication(); //legge JWT dal header, valida token, popola HttpContext.User
app.UseAuthorization();  //applica [Authorize]
app.UseHealthChecksConfiguration();

app.MapControllers();  //collega routing -> controller
app.Run();

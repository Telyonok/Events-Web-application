using AuthorizationServer.Domain.Models;
using EventsWebApp.Application.AutoMapperProfiles;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Services;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.UnitOfWork;
using EventsWebApp.Presentation.Middleware;
using EventsWebApp.Presentation.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventParticipantService, EventParticipantService>();
builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddScoped<IValidator<EventParticipant>, EventParticipantValidator>();

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventsDbContext")));

builder.Services.AddAuthentication(builder.Configuration.GetSection("AppSettings").Get<AppSettings>());

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new EventProfile());
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new EventParticipantProfile());
});

builder.Services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

var app = builder.Build();

app.UseCustomExceptionHandlerMiddlware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

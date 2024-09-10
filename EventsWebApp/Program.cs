using AuthorizationServer.Domain.Models;
using EventsWebApp.Application.AutoMapperProfiles;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.UnitOfWork;
using EventsWebApp.Presentation.Middleware;
using EventsWebApp.Presentation.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddApplicationServices();  // UseCases.

builder.Services.AddScoped<IValidator<CreateEventDTO>, CreateEventDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateEventDTO>, UpdateEventDTOValidator>();
builder.Services.AddScoped<IValidator<CreateEventParticipantDTO>, CreateEventParticipantDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateEventParticipantDTO>, UpdateEventParticipantDTOValidator>();

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

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

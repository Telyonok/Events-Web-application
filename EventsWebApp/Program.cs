using EventsWebApp.Application.AutoMapperProfiles;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.UnitOfWork;
using EventsWebApp.Presentation.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventParticipantService, EventParticipantService>();

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventsDbContext")));

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

app.UseAuthorization();

app.MapControllers();

app.Run();

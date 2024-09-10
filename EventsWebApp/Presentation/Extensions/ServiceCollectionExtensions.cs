using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.UseCases.EventParticipantUseCases;
using EventsWebApp.Application.UseCases.EventUseCases;

namespace EventsWebApp.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Event UseCases.
            services.AddScoped<IAddEventUseCase, AddEventUseCase>();
            services.AddScoped<IGetAllEventsUseCase, GetAllEventsUseCase>();
            services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
            services.AddScoped<IGetEventByTitleUseCase, GetEventByTitleUseCase>();
            services.AddScoped<IGetEventsWithFilterUseCase, GetEventsWithFilterUseCase>();
            services.AddScoped<IGetAllEventsPagedUseCase, GetAllEventsPagedUseCase>();
            services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();
            services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();

            // EventParticipant UseCases.
            services.AddScoped<IAddEventParticipantUseCase, AddEventParticipantUseCase>();
            services.AddScoped<IGetAllEventParticipantsUseCase, GetAllEventParticipantsUseCase>();
            services.AddScoped<IGetAllEventParticipantsPagedUseCase, GetAllEventParticipantsPagedUseCase>();
            services.AddScoped<IGetEventParticipantByIdUseCase, GetEventParticipantByIdUseCase>();
            services.AddScoped<IGetEventParticipantsByEventIdUseCase, GetEventParticipantsByEventIdUseCase>();
            services.AddScoped<IRegisterEventParticipantUseCase, RegisterEventParticipantUseCase>();
            services.AddScoped<IUnRegisterEventParticipantUseCase, UnRegisterEventParticipantUseCase>();
            services.AddScoped<IUpdateEventParticipantUseCase, UpdateEventParticipantUseCase>();
            services.AddScoped<IDeleteEventParticipantUseCase, DeleteEventParticipantUseCase>();
        }
    }
}

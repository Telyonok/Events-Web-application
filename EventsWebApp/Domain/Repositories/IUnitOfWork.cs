namespace EventsWebApp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        IEventParticipantRepository EventParticipants { get; }

        Task CompleteAsync();
    }
}

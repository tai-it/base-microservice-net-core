namespace Simple.EventBus.Abstractions
{
    using Simple.EventBus.Events;

    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
    }
}

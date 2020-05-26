namespace Simple.Catalog.Api.Services.Categories
{
    using Simple.EventBus.Events;

    public class CategoryCreatedEvent : IntegrationEvent
    {
        public string Name { get; set; }

        public CategoryCreatedEvent(string name)
        {
            this.Name = name;
            this.RoutingKey = "crud_category_created";
        }
    }
}

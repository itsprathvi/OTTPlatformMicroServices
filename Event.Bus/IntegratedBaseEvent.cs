namespace Event.Bus
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        //similar to event id to be tracked in a queueing tool like RabbitMQ
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }

}

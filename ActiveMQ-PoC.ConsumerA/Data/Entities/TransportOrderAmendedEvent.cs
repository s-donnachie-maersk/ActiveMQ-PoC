using ActiveMQ_PoC.Shared.Interfaces.Events;

namespace ActiveMQ_PoC.ConsumerA.Data.Entities
{
    public class TransportOrderAmendedEvent : ITransportOrderAmendedEvent
    {
        public TransportOrderAmendedEvent(ITransportOrderAmendedEvent e)
        {
            this.DatabaseId = e.DatabaseId;
            this.BlobUrl = e.BlobUrl;
            this.ReferenceId = e.ReferenceId;
            this.EventDateTime = e.EventDateTime;
        }

        public TransportOrderAmendedEvent()
        {
            
        }

        public int DatabaseId { get; set; }
        public string ReferenceId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string BlobUrl { get; set; }
    }
}

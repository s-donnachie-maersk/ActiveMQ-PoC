namespace ActiveMQ_PoC.ConsumerA.Data.Entities
{
    public class TransportOrder
    {
        public string ReferenceId { get; set; }
        public int DatabaseId { get; set; }
        public string Status { get; set; }
        public string BlobUrl { get; set; }
    }
}

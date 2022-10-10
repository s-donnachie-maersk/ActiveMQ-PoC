namespace ActiveMQ_PoC.ConsumerA.Data.Entities;

public class TransportOrderDoc
{
 
    public int Id { get; set; }
    public TransportOrder TransportOrder { get; set; }
    public TransportOrderAmendedEvent OriginalEvent { get; set; }
}
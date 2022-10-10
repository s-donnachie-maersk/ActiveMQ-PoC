using System.Text.Json;
using ActiveMQ_PoC.ConsumerA.Data.Context;
using ActiveMQ_PoC.ConsumerA.Data.Entities;
using ActiveMQ_PoC.Shared.Interfaces;
using ActiveMQ_PoC.Shared.Interfaces.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ActiveMQ_PoC.ConsumerA.Consumers
{
    public class TransportOrderAmendedEventConsumer : IConsumer<ITransportOrderAmendedEvent>
    {
        private readonly AppDbContext _db;

        public TransportOrderAmendedEventConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<ITransportOrderAmendedEvent> context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                $"\nReceived TransportOrderAmendedEvent : {JsonSerializer.Serialize(context.Message)} with ReferenceId : {context.Message.ReferenceId}");


            var existing =
                await _db.TransportOrderDocs.FirstOrDefaultAsync(
                    x => x.TransportOrder.ReferenceId == context.Message.ReferenceId);

            if (existing == null)
            {
                await _db.AddAsync(new TransportOrderDoc
                {
                    Id = context.Message.DatabaseId,
                    TransportOrder = new TransportOrder
                    {
                        BlobUrl = context.Message.BlobUrl,
                        DatabaseId = context.Message.DatabaseId,
                        ReferenceId = context.Message.ReferenceId
                    },
                    OriginalEvent = new TransportOrderAmendedEvent(context.Message)
                });
            }
            else
            {
                existing.TransportOrder = new TransportOrder
                {
                    BlobUrl = context.Message.BlobUrl,
                    DatabaseId = context.Message.DatabaseId,
                    Status = "Updated",
                    ReferenceId = context.Message.ReferenceId
                };

                _db.Update(existing);
            }

            await _db.SaveChangesAsync();

            //await _db.AddAsync(new TransportOrderDoc
            //{
            //    Id = context.Message.DatabaseId,
            //    TransportOrder = new TransportOrder
            //    {
            //        BlobUrl = context.Message.BlobUrl,
            //        DatabaseId = context.Message.DatabaseId,
            //        ReferenceId = context.Message.ReferenceId
            //    }
            //});

            //await _db.SaveChangesAsync();
            //await _db.TransportOrderDocs.Upsert(
            //        new TransportOrderDoc
            //        {
            //            Id = context.Message.DatabaseId,
            //            TransportOrder = new TransportOrder
            //            {
            //                BlobUrl = context.Message.BlobUrl,
            //                DatabaseId = context.Message.DatabaseId,
            //                ReferenceId = context.Message.ReferenceId
            //            }
            //        })
            //    .On(v => new { v.ReferenceId })
            //    .WhenMatched((old, @new) => new TransportOrderDoc
            //    {
            //        TransportOrder = @new.TransportOrder
            //    })
            //    .RunAsync();
        }
    }
}

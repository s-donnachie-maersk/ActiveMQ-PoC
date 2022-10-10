using System.Text.Json;
using ActiveMQ_PoC.ConsumerA.Data.Context;
using ActiveMQ_PoC.Shared.Interfaces.Requests;
using Bogus;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ActiveMQ_PoC.ConsumerA.Consumers;

public class GetTransportOrderStatusConsumer : IConsumer<IGetTransportOrderStatusRequest>
{
    private readonly AppDbContext _db;

    public GetTransportOrderStatusConsumer(AppDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<IGetTransportOrderStatusRequest> context)
    {
        var response =
            await _db.TransportOrderDocs.FirstOrDefaultAsync(x =>
                x.TransportOrder.ReferenceId == context.Message.ReferenceId);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(
            $"\nReceived IGetTransportOrderStatusRequest request {JsonSerializer.Serialize(context.Message)}");

        await context.RespondAsync<ITransportOrderStatusResponse>(new { response.TransportOrder.ReferenceId,response.TransportOrder.BlobUrl, response.TransportOrder.DatabaseId, response.TransportOrder.Status });

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nResponded with {JsonSerializer.Serialize(response)}");
    }
}
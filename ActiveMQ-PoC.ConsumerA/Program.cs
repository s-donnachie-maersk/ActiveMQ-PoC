using ActiveMQ_PoC.ConsumerA.Consumers;
using ActiveMQ_PoC.ConsumerA.Data.Context;
using ActiveMQ_PoC.Shared.Constants;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Drawing;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    var connectionString =
                        "Host=localhost;Database=ActiveMQ;Integrated Security=True;Username=postgres;Include Error Detail=true;Maximum Pool Size=10";
                    connectionString = $"{connectionString};TrustServerCertificate=true;";
                    options.UseNpgsql(connectionString, builder =>
                    {
                        builder.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                    });
                });

                services.AddMassTransit(x =>
                {
                    x.AddConsumer<TransportOrderAmendedEventConsumer>();
                    x.AddConsumer<GetTransportOrderStatusConsumer>();

                    x.UsingActiveMq((context, cfg) =>
                    {
                        cfg.Host("localhost", 61616, h =>
                        {  
                            h.Username("admin");
                            h.Password("admin");
                        });

                        cfg.ReceiveEndpoint(QueueName.ConsumerA, e =>
                        {
                            e.ConfigureConsumer<TransportOrderAmendedEventConsumer>(context);
                            e.ConfigureConsumer<GetTransportOrderStatusConsumer>(context);
                        });
                    });
                });
            })
            .Build()
            .RunAsync();

        Console.Write("\nPress 'Enter' to exit the process...");
  
        // another use of "Console.ReadKey()" method
        // here it asks to press the enter key to exit
        while (Console.ReadKey().Key != ConsoleKey.Enter) {

        }
    }
}
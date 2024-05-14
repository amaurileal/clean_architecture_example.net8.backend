using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MotorcycleRentMessageBrokerConsumer1.Domain;
using MotorcycleRentMessageBrokerConsumer1.Domain.Repositories;
using MotorcycleRentMessageBrokerConsumer1.Infrastructure.Persistence;
using MotorcycleRentMessageBrokerConsumer1.Infrastructure.Repositories;
using MotorcycleRentMessageBrokerConsumer1.Infrastructure.Services;
using Serilog;

namespace MotorcycleRentMessageBrokerConsumer1;

class Program
{
    public delegate RabbitMQConsumer RabbitMQConsumerFactory(string hostname, string queueName, string username, string password);

    static void Main(string[] args)
    {
        //Setting Configuration File
        var builder = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();


        //Setting logger Configuration
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Day)
        .CreateLogger();

        Log.Information("Starting the application");
        try
        {


            // Configuração de DI
            var services = new ServiceCollection()
            .AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true))  // Configure Serilog as a logging provider
            .AddDbContext<MortorcycleRentMessageBrokerCusomer1DbContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:MotorcycleRentDb"]))
            .AddSingleton<IMotorcycle2024Repository, Motorcycle2024Repository>()
            .AddSingleton<IEmailService, EmailService>()
            .AddTransient<RabbitMQConsumer>(provider =>
                new RabbitMQConsumer(provider.GetRequiredService<IMotorcycle2024Repository>(),
                provider.GetRequiredService<IEmailService>()
                , provider.GetRequiredService<ILogger<RabbitMQConsumer>>(),
                "localhost", "fila_moto2024", "guest", "guest"))
            .AddTransient<RabbitMQConsumerFactory>(provider =>
                (string hostname, string queueName, string username, string password) => provider.GetRequiredService<RabbitMQConsumer>());

            var provider = services.BuildServiceProvider();

            // Getting DbContext
            using (var context = provider.GetRequiredService<MortorcycleRentMessageBrokerCusomer1DbContext>())
            {
                // applying migrations
                DatabaseInitializer.MigrateDatabase(context);


                // Using Factory to get the consumer
                var rabbitMQConsumerFactory = provider.GetRequiredService<RabbitMQConsumerFactory>();
                var rabbitMQConsumer = rabbitMQConsumerFactory("localhost", "fila_moto2024", "guest", "guest");

                // Start Consumer
                rabbitMQConsumer.Start();

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Caught an exception during operation");
        }

        Log.Information("Ending the application");

        // Ensure logs are finalized and downloaded
        Log.CloseAndFlush();
    }
}
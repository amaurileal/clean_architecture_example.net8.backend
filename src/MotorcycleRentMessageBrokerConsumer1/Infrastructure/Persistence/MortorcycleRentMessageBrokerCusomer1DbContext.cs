using Microsoft.EntityFrameworkCore;
using MotorcycleRentMessageBrokerConsumer1.Domain.Entities;

namespace MotorcycleRentMessageBrokerConsumer1.Infrastructure.Persistence;

internal class MortorcycleRentMessageBrokerCusomer1DbContext(DbContextOptions<MortorcycleRentMessageBrokerCusomer1DbContext> options) : DbContext(options)
{
    internal DbSet<Motorcycle2024> Motorcycles2024 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;User Id=root;Password=root;Database=db_motorcyclerent2;");
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Infrastructure.Persistence;

internal class MotorcycleRentalDbContext(DbContextOptions<MotorcycleRentalDbContext> options) 
    :  IdentityDbContext<User>(options)
{   

    internal DbSet<Motorcycle> Motorcycles { get; set; }
    internal DbSet<RentPlan> RentPlans { get; set;}

    internal DbSet<Biker> Bikers { get; set; }
    internal DbSet<Rent> Rents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Motorcycle>()
        .HasIndex(u => u.LicensePlate)
        .IsUnique();

        modelBuilder.Entity<Motorcycle>()
            .Property(entity => entity.Status)
            .HasComment("Available Values: A=Active, R=Rented, S=Stopped");

        modelBuilder.Entity<Motorcycle>()
            .HasMany(m => m.Rents)
            .WithOne(r => r.Motorcycle)
            .HasForeignKey(r => r.MotorcycleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RentPlan>()
            .HasMany(m => m.Rents)
            .WithOne(r => r.RentPlan)
            .HasForeignKey(r => r.RentPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Biker>()
         .HasMany(m => m.Rents)
         .WithOne(r => r.Biker)
         .HasForeignKey(r => r.BikerId)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Biker>()
            .HasIndex(u => u.CNPJ)
            .IsUnique();

        modelBuilder.Entity<Biker>()
            .HasIndex(u => u.CNH)
            .IsUnique();

        modelBuilder.Entity<Rent>()
            .HasOne(r => r.Biker);
            

    }


}

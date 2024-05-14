using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Infrastructure.Persistence;

namespace MotorcycleRental.Infrastructure.Seeders
{
    internal class MotorcycleRentalSeeder(
        MotorcycleRentalDbContext dbContext,
        UserManager<User> userManager) : IMotorcycleRentalSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }


            if (await dbContext.Database.CanConnectAsync())
            {


                if (!dbContext.Roles.Any())
                {
                    var roles = getRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Users.Any())
                {
                    await InsertUsersWithRoles();

                }

                if (!dbContext.RentPlans.Any())
                {
                    var rentalPlans = getRentalPlans();
                    dbContext.RentPlans.AddRange(rentalPlans);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Motorcycles.Any())
                {
                    var motorcycles = getMotorcycles();
                    dbContext.Motorcycles.AddRange(motorcycles);
                    await dbContext.SaveChangesAsync();
                }                
            }
        }

        private async Task InsertUsersWithRoles()
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var userAdmin = new User();
                userAdmin.Email = "admin@test.com";
                userAdmin.UserName = "admin@test.com";
                userAdmin.NormalizedUserName = "admin@test.com".ToUpper();
                userAdmin.NormalizedEmail = "admin@test.com".ToUpper();
                userAdmin.LockoutEnabled = false;

                var resultUser = await userManager.CreateAsync(userAdmin, "Password!1");

                if (!resultUser.Succeeded)
                {
                    throw new Exception("Failed to create User");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(userAdmin, UserRoles.Admin);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role: {UserRoles.Admin} to user");
                }

                var userBiker = new User()
                {
                    Email = "biker@test.com",
                    UserName = "biker@test.com",
                    NormalizedUserName = "biker@test.com".ToUpper(),
                    NormalizedEmail = "biker@test.com".ToUpper(),
                    LockoutEnabled = false
            };

                resultUser = await userManager.CreateAsync(userBiker, "Password!1");

                if (!resultUser.Succeeded)
                {
                    throw new Exception("Failed to create User");
                }

                addToRoleResult = await userManager.AddToRoleAsync(userBiker, UserRoles.Biker);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role: {UserRoles.Biker} to user");
                }

                Biker biker = new Biker()
                {
                    CNH = "99999999999",
                    CNPJ = "98.435.634/0001-71",
                    DateOfBirth = new DateOnly(1982, 1, 16),
                    User = userBiker,
                    CNHType = "AB"
                };

                dbContext.Bikers.Add(biker);
                await dbContext.SaveChangesAsync();

                transaction.Commit();               

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }
        private IEnumerable<RentPlan> getRentalPlans()
        {
            List<RentPlan> rentalPlans = [
                new(){
                    Cost = 30,
                    Days = 7,
                    Description= "7 dias com um custo de R$30,00 por dia"
                },
                new(){
                    Cost = 28,
                    Days = 15,
                    Description= "15 dias com um custo de R$28,00 por dia"
                },
                new(){
                    Cost = 22,
                    Days = 30,
                    Description= "30 dias com um custo de R$22,00 por dia"
                },
                new(){
                    Cost = 20,
                    Days = 45,
                    Description= "45 dias com um custo de R$20,00 por dia"
                },
                new(){
                    Cost = 18,
                    Days = 50,
                    Description= "50 dias com um custo de R$18,00 por dia"
                }
                ];

            return rentalPlans;
        }

        private IEnumerable<IdentityRole> getRoles()
        {
            List<IdentityRole> roles = [
                new(UserRoles.Admin){
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new(UserRoles.Biker){
                    NormalizedName = UserRoles.Biker.ToUpper()
                }
                ];

            return roles;
        }

        private IEnumerable<Motorcycle> getMotorcycles()
        {
            List<Motorcycle> motorcycles = [
                new()
                {
                    Description = "Honda CB 300",
                    Model = 2022,
                    Year =  2023,
                    LicensePlate = "ABC-1234",
                    Status = "A"
                },
                new()
                {
                    Description = "Yamaha Factor 125",
                    Model = 2020,
                    Year =  2021,
                    LicensePlate = "ABC-1B23",
                    Status = "A"
                }
                ];
            return motorcycles;
        }
    }
}

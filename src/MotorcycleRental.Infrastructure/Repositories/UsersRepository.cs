using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using MotorcycleRental.Infrastructure.Persistence;

namespace MotorcycleRental.Infrastructure.Repositories
{
    internal class UsersRepository(
        UserManager<User> userManager,
        MotorcycleRentalDbContext dbContext) : IUsersRepository
    {
        public async Task<string> InsertAdmin(User entity, string password)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var createUserResult = await userManager.CreateAsync(entity, password);
                if (!createUserResult.Succeeded)
                {
                    throw new Exception("Failed to create user");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(entity, UserRoles.Admin);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role: {UserRoles.Admin} to user");
                }
                await transaction.CommitAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

        }


        public async Task<string> InsertBiker(User entity, string password, Biker biker)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var createUserResult = await userManager.CreateAsync(entity, password);
                if (!createUserResult.Succeeded)
                {
                    throw new Exception("Failed to create user");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(entity, UserRoles.Admin);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role: {UserRoles.Admin} to user");
                }

                biker.User = entity;

                dbContext.Bikers.Add(biker);

                await dbContext.SaveChangesAsync();

                transaction.Commit();

                return entity.Id;
            }
            catch (Exception e)
            {

                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using MotorcycleRental.Infrastructure.Persistence;

namespace MotorcycleRental.Infrastructure.Repositories
{
    internal class RentsRepository(MotorcycleRentalDbContext dbContext) : IRentsRepository
    {
        public async Task<int> Create(Rent entity)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {

                dbContext.Rents.Add(entity);
                await dbContext.SaveChangesAsync();

                entity.Motorcycle.Status = "R";
                await dbContext.SaveChangesAsync();

                transaction.Commit();
                return entity.Id;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Rent?> GetActiveRentByBiker(int bikerId)
        {   
                return await dbContext.Rents.Where(x => x.Biker.Id == bikerId && x.FinalDate == null)
                    .Include(r => r.RentPlan).FirstOrDefaultAsync();
        }

        public async Task<Rent?> GetByIdAndByBikerIdAsync(int rentId, int bikerId)
        {
            var rent = await dbContext.Rents.Where(x => x.Id == rentId && x.BikerId == bikerId)
                .Include(r => r.RentPlan).Include(r => r.Motorcycle).ToListAsync();

            return rent.FirstOrDefault();
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}

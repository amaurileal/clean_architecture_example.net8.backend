using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using MotorcycleRental.Infrastructure.Persistence;

namespace MotorcycleRental.Infrastructure.Repositories
{
    internal class RentPlansRepository(MotorcycleRentalDbContext dbContext) : IRentPlansRepository
    {
        public async Task<int> Create(RentPlan entity)
        {
            dbContext.RentPlans.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(RentPlan entity)
        {
            dbContext.RentPlans.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentPlan>> GetAllByAsync()
        {
            var rentalPlans = await dbContext.RentPlans.ToListAsync();            
            return rentalPlans;
        }

        public async Task<RentPlan?> GetByIdAsync(int id)
        {
            var rentalPlan = await dbContext.RentPlans.FirstOrDefaultAsync(x => x.Id == id);
            return rentalPlan;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}

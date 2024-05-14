using MotorcycleRentMessageBrokerConsumer1.Domain.Entities;
using MotorcycleRentMessageBrokerConsumer1.Domain.Repositories;
using MotorcycleRentMessageBrokerConsumer1.Infrastructure.Persistence;

namespace MotorcycleRentMessageBrokerConsumer1.Infrastructure.Repositories
{
    internal class Motorcycle2024Repository(MortorcycleRentMessageBrokerCusomer1DbContext dbContext) : IMotorcycle2024Repository
    {
        public async Task<int> Create(Motorcycle2024 entity)
        {
            dbContext.Motorcycles2024.Add(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}

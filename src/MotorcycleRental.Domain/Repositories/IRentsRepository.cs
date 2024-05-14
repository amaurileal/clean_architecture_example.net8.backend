using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Repositories
{
    public interface IRentsRepository
    {
        Task<int> Create(Rent entity);

        Task<Rent?> GetActiveRentByBiker(int bikerId);

        Task<Rent?> GetByIdAndByBikerIdAsync(int rentId, int bikerId);

        Task SaveChanges();

    }
}

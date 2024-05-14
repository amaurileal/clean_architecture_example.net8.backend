using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Repositories
{
    public interface IRentPlansRepository
    {
        Task<IEnumerable<RentPlan>> GetAllByAsync();

        Task<RentPlan?> GetByIdAsync(int id);

        Task<int> Create(RentPlan entity);

        Task Delete(RentPlan entity);

        Task SaveChanges();


    }
}

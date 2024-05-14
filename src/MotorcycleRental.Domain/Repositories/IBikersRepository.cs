using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Repositories
{
    public interface IBikersRepository
    {
        Task<Biker?> GetByIdAsync(int Id);

        Task<Biker?> GetByUserIdAsync(string UserId);

        Task<IEnumerable<Biker>> GetBikerByCNPJOrCNH(Biker entity);

        Task SaveChanges();
    }
}

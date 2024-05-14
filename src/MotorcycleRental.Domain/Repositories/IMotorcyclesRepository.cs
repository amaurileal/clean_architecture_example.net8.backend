using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using System.ComponentModel;

namespace MotorcycleRental.Infrastructure.Repositories
{
    public interface IMotorcyclesRepository
    {
        Task<IEnumerable<Motorcycle>> GetAllAsync();

        Task<Motorcycle?> GetByIdAsync(int id);

        Task<int> Create(Motorcycle entity);

        Task Delete(Motorcycle entity);

        Task SaveChanges();

        Task<(IEnumerable<Motorcycle>, int)> GetAllOrByLicensePlateAsync(string? licensePlate, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);

        Task<(IEnumerable<Motorcycle>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);

        Task<(IEnumerable<Motorcycle>, int)> GetAllActivesMotorcyclesAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    }
}
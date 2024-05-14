using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace MotorcycleRental.Infrastructure.Repositories
{
    internal class MotorcyclesRepository(MotorcycleRentalDbContext dbContext) : IMotorcyclesRepository
    {
        public async Task<int> Create(Motorcycle entity)
        {
            dbContext.Motorcycles.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Motorcycle entity)
        {
            dbContext.Motorcycles.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Motorcycle>, int)> GetAllActivesMotorcyclesAsync(
            string? searchPhrase,
            int pageSize,
            int pageNumber,
            string? sortBy,
            SortDirection sortDirection
            )
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Motorcycles
                .Where(r => (searchPhraseLower == null || (r.LicensePlate.ToLower().Contains(searchPhraseLower)
                                                       || r.Model.ToString().Contains(searchPhraseLower)
                                                       || r.Year.ToString().Contains(searchPhraseLower)))
                                                       
                                                       && r.Status == "A");

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Motorcycle, object>>>
            {
                    { nameof(Motorcycle.Id), r => r.Id},
                { nameof(Motorcycle.Model), r => r.Model},
                { nameof(Motorcycle.Year), r => r.Year},
                { nameof(Motorcycle.LicensePlate), r => r.LicensePlate },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var motorcycles = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (motorcycles, totalCount);
        }

        public async Task<IEnumerable<Motorcycle>> GetAllAsync()
        {
            var motorcycles = await dbContext.Motorcycles.ToListAsync();
            return motorcycles;
        }

        public async Task<(IEnumerable<Motorcycle>, int)> GetAllMatchingAsync(
            string? searchPhrase, 
            int pageSize, 
            int pageNumber, 
            string? sortBy, 
            SortDirection sortDirection
            )
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Motorcycles
                .Where(r => searchPhraseLower == null || (r.LicensePlate.ToLower().Contains(searchPhraseLower)
                                                       || r.Model.ToString().Contains(searchPhraseLower)
                                                       || r.Year.ToString().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Motorcycle, object>>>
            {
                    { nameof(Motorcycle.Id), r => r.Id},
                { nameof(Motorcycle.Model), r => r.Model},
                { nameof(Motorcycle.Year), r => r.Year},
                { nameof(Motorcycle.LicensePlate), r => r.LicensePlate },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var motorcycles = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (motorcycles, totalCount);
        }

        public async Task<(IEnumerable<Motorcycle>, int)> GetAllOrByLicensePlateAsync(
            string? licensePlate,
            int pageSize,
            int pageNumber,
            string? sortBy,
            SortDirection sortDirection
            )
        {

            var licensePlateLower = licensePlate?.ToLower();

            //filtering by licensePlate
            var baseQuery = dbContext
                .Motorcycles
                .Where(r => licensePlateLower == null || (r.LicensePlate.ToLower().Contains(licensePlateLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Motorcycle, object>>>
            {
                { nameof(Motorcycle.Model), r => r.Model},
                { nameof(Motorcycle.Year), r => r.Year},
                { nameof(Motorcycle.LicensePlate), r => r.LicensePlate },
                { nameof(Motorcycle.Id), r => r.Id },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var motorcycles = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (motorcycles, totalCount);
        }

        public async Task<Motorcycle?> GetByIdAsync(int id)
        {
            var motor = await dbContext.Motorcycles.FirstOrDefaultAsync(x => x.Id == id);
            return motor;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
        
    }
}

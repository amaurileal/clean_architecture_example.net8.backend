using Microsoft.AspNetCore.Identity;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Repositories
{
    public interface IUsersRepository
    {
          Task<string> InsertBiker(User entity, string password,Biker biker);

        Task<string> InsertAdmin(User entity, string password);

        
    }
}

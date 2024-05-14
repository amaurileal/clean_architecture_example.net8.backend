using MotorcycleRentMessageBrokerConsumer1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRentMessageBrokerConsumer1.Domain.Repositories
{
    public interface IMotorcycle2024Repository
    {
        Task<int> Create(Motorcycle2024 entity);
    }
}

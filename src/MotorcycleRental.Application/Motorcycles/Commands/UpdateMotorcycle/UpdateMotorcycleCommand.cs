using MediatR;
using MotorcycleRental.Application.Rents.Dtos;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle
{
    public class UpdateMotorcycleCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string LicensePlate { get; set; } = default!;


    }

}
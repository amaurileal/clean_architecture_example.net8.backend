using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Application.Bikers.Dtos
{
    public class BikerDto
    {

        public int Id { get; set; }
        public string CNPJ { get; set; } = default!;

        public DateOnly DateOfBirth { get; set; }

        public string CNH { get; set; } = default!;

        public string CNHType { get; set; }

        public string? CHNImg { get; set; }

        public string? CNHUrl { get; set; }

    }
}

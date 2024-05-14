using System.Text.Json.Serialization;

namespace MotorcycleRental.Application.Motorcycles.Dtos
{
    public class MotorcycleDto
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public int Model { get; set; } = default!;

        public string LicensePlate { get; set; } = default!;

        public string Status { get; set; } = default!;
    }
}

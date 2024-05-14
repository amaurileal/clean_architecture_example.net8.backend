using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Application.Bikers.Dtos
{
    public class CreateBikerDto
    {

       
        public string CNPJ { get; set; } = default!;

        public DateOnly DateOfBirth { get; set; }

        public string CNH { get; set; } = default!;

        public string CNHType { get; set; } = default!;
       

    }
}

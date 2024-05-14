
using System.Text.Json.Serialization;

namespace MotorcycleRental.Application.RentPlans.Dtos
{
    public class RentPlanDto
    {
        
        public int Id { get; set; }
       
        public string Description { get; set; } = default!;
        
        public int Days { get; set; }

       
        public decimal Cost { get; set; }
    }
}

namespace MotorcycleRental.Domain.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public RentPlan RentPlan { get; set; } = new();

        public int RentPlanId{ get; set; }

        public Biker Biker { get; set; } = new();

        public int BikerId { get; set; }
                
        public Motorcycle Motorcycle { get; set; } = new();

        public int MotorcycleId { get; set; }

        public DateOnly InitialDate { get; set; }

        public DateOnly? FinalDate { get; set; }

        public DateOnly PreviewDate { get; set; }

        public decimal? Total { get; set; }


    }
}

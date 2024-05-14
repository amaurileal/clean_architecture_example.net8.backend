using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRentMessageBrokerConsumer1.Domain.Entities
{
    public class Motorcycle2024
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        
        
        public int Model { get; set; } = default!;

        public string? Description { get; set; }

        
        public string LicensePlate { get; set; } = default!;

        
        public string Status { get; set; } = default!;

        public DateTime CreateDate { get; set; } = new DateTime();
    }
}

using MotorcycleRental.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Domain.Entities
{
    public class Biker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(18)]
        [Required]
        public string CNPJ { get; set; } = default!;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [MaxLength(11)]
        [Required]
        public string CNH { get; set; } = default!;

        [MaxLength(2)]
        [Required]
        public string CNHType { get; set; }

        public string? CHNImg { get; set; }

        public List<Rent> Rents { get; set; } = [];

        public User User { get; set; }

        public string UserId { get; set; }



    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleRental.Domain.Entities;

public class Motorcycle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]    
    public int Year { get; set; }

    [Required]    
    public int Model { get; set; } = default!;
    
    public string? Description { get; set; }

    [Required]
    [MaxLength(10)]    
    public string LicensePlate { get; set; } = default!;

    [Required]
    public string Status { get; set; } = default!;

    public List<Rent> Rents { get; set; } = [];
}

using MediatR;

namespace MotorcycleRental.Application.Rents.Queries.CheckRentValue
{
    public class CheckRentValueQuery(DateOnly previewDate) : IRequest<decimal>
    {
        public DateOnly PreviewDate { get; set; } = previewDate;
    }
}

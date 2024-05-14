using FluentValidation;
using MotorcycleRental.Application.Motorcycles.Dtos;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles;

public class GetAllMotorcyclesQueryValidator : AbstractValidator<GetAllMotorcyclesQuery>
{
    private int[] allowPageSizes = [5, 10, 15, 30];
    private string[] allowedSortByColumnNames = [nameof(MotorcycleDto.Model),
        nameof(MotorcycleDto.LicensePlate),
        nameof(MotorcycleDto.Year),
        nameof(MotorcycleDto.Id)];


    public GetAllMotorcyclesQueryValidator()
    {
        RuleFor(dto => dto.LicensePlate).Matches(@"^([A-Za-z]{3}-[0-9]{4}|[A-Za-z]{3}-[0-9][A-Za-z][0-9]{2})$")
                    .WithMessage("License Plate Format Invalid! It Must be (###-####).");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}

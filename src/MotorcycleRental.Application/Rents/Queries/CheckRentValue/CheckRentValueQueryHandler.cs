using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Rents.Queries.CheckRentValue
{
    public class CheckRentValueQueryHandler(
        IRentsRepository rentsRepository,
        IBikersRepository bikersRepository,
        ILogger<CheckRentValueQueryHandler> logger,
        IUserContext userContext

        ) : IRequestHandler<CheckRentValueQuery, decimal>
    {
        
        public async Task<decimal> Handle(CheckRentValueQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{BikerEmail} is verifying invoice by rent final date preview: {datePreview}",
                currentUser.Email, request.PreviewDate);

            var biker = await bikersRepository.GetByUserIdAsync(currentUser.Id);
            if (biker == null)
            {
                throw new NotFoundException(nameof(Biker), $"Biker for userid:{currentUser.Id}");
            }

            var rent = await rentsRepository.GetActiveRentByBiker(biker.Id);
            if (rent == null)
            {
                throw new NotFoundException(nameof(Rent), $"Active Rent for Biker CNH:{biker.CNH}");
            }

            var calculatedValue = getCalculateValue(rent, request.PreviewDate);

            return calculatedValue;
        }

        /*
            Quando a data informada for inferior a data prevista do término, será cobrado o valor das diárias e uma multa adicional
            Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
            Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
            Quando a data informada for superior a data prevista do término, será cobrado um valor adicional de R$50,00 por diária adicional.
         */
        private decimal getCalculateValue(Rent rent, DateOnly previewDate)
        {
            decimal result = 0;
            int dailyRates = previewDate.DayNumber - rent.InitialDate.DayNumber;
            if (previewDate < rent.PreviewDate)
            {
                
                if (rent.RentPlan.Days <= 7)
                {
                    result = dailyRates * rent.RentPlan.Cost * 1.2M;
                }
                else
                {
                    result = dailyRates * rent.RentPlan.Cost * 1.4M;
                }
            }
            else
            {
                int extraDaily = previewDate.DayNumber - rent.PreviewDate.DayNumber;
                result = (rent.RentPlan.Cost * rent.RentPlan.Days) +  extraDaily * 50;
            }

            return result;
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Application.Bikers.Commands.SendCHNImage
{
    public class SendCNHImageCommandValidator : AbstractValidator<SendCNHImageCommand>
    {
        public SendCNHImageCommandValidator()
        {
            RuleFor(dto => dto.CNHImage).NotEmpty();
        }
    }
}

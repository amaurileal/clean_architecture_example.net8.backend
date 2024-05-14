using FluentValidation;

namespace MotorcycleRental.Application.Users.Commands.BikerRegister
{
    public class BikerRegisterCommandValidator : AbstractValidator<BikerRegisterCommand>
    {
        
        private readonly List<string?> ValidCNHTypes = ["A", "B", "AB"];

        public BikerRegisterCommandValidator()
        {           

            RuleFor(dto => dto.Email).EmailAddress();
           
            RuleFor(dto => dto.CreateBikerDto).NotNull();

            RuleFor(dto => dto.CreateBikerDto.CNH)
                .Matches(@"^\d{11}$")
                .WithMessage("Invalid CHN. Please provide 11 numbers, only numbers for CHN (999999999)");

            RuleFor(dto => dto.CreateBikerDto.CNPJ)
                //.NotEmpty()
                .Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")
                .WithMessage("Please provide a valid CNPJ (99.999.999/9999-99).")
                .Must(BeAValidCNPJ).WithMessage("CNPJ inválido.");

            RuleFor(dto => dto.CreateBikerDto.CNHType)
           .Must(ValidCNHTypes.Contains)
           .WithMessage("Invalid CHNType. Please choose from the valid types(A,B or AB).");

            RuleFor(dto => dto.CreateBikerDto.DateOfBirth)
                .NotEmpty()
                .LessThan(DateOnly.FromDateTime(DateTime.Now).AddYears(-18))
                .WithMessage("Delivery driver must be over 18 years old");
        }


        private bool BeAValidCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            var cleanCnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cleanCnpj.Length != 14)
                return false;

            // Dígitos conhecidos inválidos
            string[] knownInvalids = { "00000000000000", "11111111111111", "22222222222222",
                                   "33333333333333", "44444444444444", "55555555555555",
                                   "66666666666666", "77777777777777", "88888888888888",
                                   "99999999999999" };
            if (knownInvalids.Contains(cleanCnpj))
                return false;

            // Cálculo do primeiro dígito verificador
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cleanCnpj[i].ToString()) * multiplier1[i];
            }

            int remainder = sum % 11;
            var digit1 = (remainder < 2) ? 0 : 11 - remainder;

            if (cleanCnpj[12] != char.Parse(digit1.ToString()))
                return false;

            // Cálculo do segundo dígito verificador
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cleanCnpj[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            var digit2 = (remainder < 2) ? 0 : 11 - remainder;

            return cleanCnpj[13] == char.Parse(digit2.ToString());
        }
    }
}

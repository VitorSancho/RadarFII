using FluentValidation;

namespace RadarFII.Business.Models.Validations
{
    public class LogPontuacaoValidation : AbstractValidator<LogPontuacao>
    {
        public LogPontuacaoValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.TarefaId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
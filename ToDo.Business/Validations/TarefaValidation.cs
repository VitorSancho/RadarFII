using FluentValidation;

namespace RadarFII.Business.Models.Validations
{
    public class TarefaValidation : AbstractValidator<Tarefa>
    {
        public TarefaValidation()
        {
            RuleFor(c => c.Hr_planejado)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .ExclusiveBetween(new TimeOnly(0, 1), new TimeOnly(23, 59))
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.UsuarioId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.DificuldadeId)
                .NotEmpty().WithMessage("A campo {PropertyName} precisa ser fornecida");
        }
    }
}
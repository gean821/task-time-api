using Api_IngaTasks.Application.Dtos.Collaborator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Validators.Collaborator
{
    public class CollaboratorCreateDtoValidator : AbstractValidator<CollaboratorCreateDto>
    {
        public CollaboratorCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Colaborador precisa de um nome.");

            RuleFor(x => x.CreatedAt)
                .NotNull()
                .WithMessage("Precisa de uma data de criação");

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Precisa de um id");
        }
    }
}

using Api_IngaTasks.Application.Dtos.Collaborator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Validators.Collaborator
{

    public class CollaboratorEditDtoValidator : AbstractValidator<CollaboratorEditDto>
    {
        public CollaboratorEditDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Não pode ser vazio.");
        }
    }
}

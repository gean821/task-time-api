using Api_IngaTasks.Application.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O id é obrigatório.");
        }
    }
}

using Api_IngaTasks.Application.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator() 
        {

            RuleFor(x => x.Password)
                .NotEmpty().MaximumLength(512);

            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}

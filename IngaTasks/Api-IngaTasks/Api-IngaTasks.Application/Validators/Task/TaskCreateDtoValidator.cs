using Api_IngaTasks.Application.Dtos.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Validators.Task
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator() 
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage("O nome da tarefa é obrigatório.");

            RuleFor(x => x.ProjectId)
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A tarefa precisa de uma descrição.");
        }
    }
}

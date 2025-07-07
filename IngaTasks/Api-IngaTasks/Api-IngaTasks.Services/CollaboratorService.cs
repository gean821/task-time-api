using Api_IngaTasks.Application.Dtos.Collaborator;
using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Dtos.User;
using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Services.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _repo;
        private readonly IValidator<CollaboratorCreateDto> _createValidator;
        private readonly IValidator<CollaboratorEditDto> _editValidator;

        public CollaboratorService(
            ICollaboratorRepository collaboratorRepository,
            IValidator<CollaboratorCreateDto> createValidator,
            IValidator<CollaboratorEditDto> editValidator)
        {
            _repo = collaboratorRepository;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        public async Task<CollaboratorDto> AdicionarColaborador(CollaboratorCreateDto dto)
        {
            var result = _createValidator.Validate(dto);
            if (!result.IsValid)
            {
                throw new ValidationException("padkpawkdaw");
            }
            var entity = new Collaborator
            {
                Name = dto.Name,
                ApplicationUser = dto.User,
                ApplicationUserId = dto.ApplicationUserId,
                CreatedAt = dto.CreatedAt,
            };
            await _repo.AdicionarColaborador(entity);

            return new CollaboratorDto
            {
                Name = entity.Name,
                ApplicationUser = entity.ApplicationUser,
                ApplicationUserId = entity.ApplicationUserId,
                CreatedAt = dto.CreatedAt
            };
        }


        public async Task<CollaboratorDto> EditarColaborador(Guid id, CollaboratorEditDto dto)
        {
            var result = _editValidator.Validate(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var entity = new Collaborator
            {
                Name = dto.Name,
                ApplicationUser = dto.ApplicationUser,
                ApplicationUserId = dto.ApplicationUserId,
                CreatedAt = dto.CreatedAt,
            };

            var collaboratorEditado = await _repo.EditarColaborador(id, entity);

            return new CollaboratorDto
            {
                Name = collaboratorEditado.Name,
                ApplicationUser = collaboratorEditado.ApplicationUser,
                ApplicationUserId = collaboratorEditado.ApplicationUserId,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                Task = dto.Task,
            };
        }




        public async Task<List<CollaboratorDto>> ListarColaboradores()
        {
            var busca = await _repo.ListarColaboradores();
            if (busca == null || busca.Count == 0)
            {
                throw new Exception("Não há colaboradores cadastrados.");
            }

            var listaDtoCollaborators = busca
                .Select(x => new CollaboratorDto
                {
                    Name = x.Name,
                    User = x.ApplicationUser,
                    ApplicationUserId = x.ApplicationUserId,
                    CreatedAt = DateTime.UtcNow,
                    Task = x.Task,
                })
            .ToList();

            return listaDtoCollaborators;
        }

        public async Task<CollaboratorDto> RemoverColaborador(Guid id)
        {
            var busca = await _repo.RemoverColaborador(id);
            if (busca == null)
            {
                throw new Exception("Não existe esse id.");
            }
            return new CollaboratorDto
            {
                Id = busca.Id,
                Name = busca.Name,
                CreatedAt = busca.CreatedAt,
                UpdatedAt = busca.UpdatedAt,
                DeletedAt = DateTime.UtcNow,
                UserId = busca.ApplicationUserId,
                User = new UserDto
                {
                    Id = Guid.Parse(busca.ApplicationUser.Id),
                    Username = busca.ApplicationUser.UserName!,
                    Email = busca.ApplicationUser.Email!,
                    CreatedAt = busca.ApplicationUser.CreatedAt
                },
                Task = busca.Task != null ? new TaskEntityDto
                {
                    Id = busca.Task.Id,
                    Name = busca.Task.Name,
                    Description = busca.Task.Description,
                    CreatedAt = busca.Task.CreatedAt,
                    ProjectName = busca.Task.Project.Name,
                    CollaboratorName = busca.Task.Collaborator?.Name
                } : null
            };
        }
    }
}


using Api_IngaTasks.Application.Dtos.Collaborator;
using Api_IngaTasks.Application.Dtos.Project;
using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Application.Validators;
using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Services.Interfaces;
using FluentValidation;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repo;
        private readonly IValidator<TaskCreateDto> _createValidator;
        private readonly IValidator<TaskEditDto> _editValidator;


        public TasksService
            (ITasksRepository repo,
            IValidator<TaskCreateDto> createValidator,
            IValidator<TaskEditDto> editValidator)
        {
            _repo = repo;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        public async Task<TaskEntityDto> AdicionarTask(TaskCreateDto dto)
        {
            var result = _createValidator.Validate(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var entity = new TaskEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                CreatedAt = DateTime.UtcNow,
                Project = dto.ProjectDto,
                TimeTrackers = dto.TimeTrackers
            };

            var taskCriada = await _repo.AdicionarTask(entity);

            await SaveChangesAsync();
            return new TaskEntityDto
            {
                Name = taskCriada.Name,
                Id = taskCriada.Id,
                Description = taskCriada.Description,
                CreatedAt = taskCriada.CreatedAt,
                ProjectName = taskCriada.Project.Name               
            };
        }
        public async Task AssociarTarefaAColaborador(TaskEntityDto dto, CollaboratorDto CollaboratorDto)
        {
            if (dto == null || CollaboratorDto == null)
            {
                throw new Exception("Dados inválidos.");
            }

            var tarefa = await _repo.BuscarTarefaPorId(dto.Id);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            var colaboradorDto = await BuscarCollaboratorPorId(CollaboratorDto.Id);
            var colaborador = new Collaborator
            {
                Id = colaboradorDto.Id,
                Name = colaboradorDto.Name,
                ApplicationUser = colaboradorDto.ApplicationUser,
                ApplicationUserId = colaboradorDto.ApplicationUserId
            };

            await _repo.AssociarTarefaAColaborador(tarefa, colaborador);

            var tarefaAtualizada = await _repo.BuscarTarefaPorId(dto.Id);
        }
        public async Task AssociarTarefaAoProjeto(TaskEntityDto dto, ProjectDto projectDto)
        {
            if (dto == null || projectDto == null)
                throw new Exception("Dados inválidos.");

            var tarefa = await _repo.BuscarTarefaPorId(dto.Id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            var projeto = await _repo.BuscarProjetoPorId(projectDto.Id);
            if (projeto == null)
                throw new Exception("Projeto não encontrado.");

            await _repo.AssociarTarefaAoProjeto(tarefa, projeto);
        }

        public async Task<CollaboratorDto> BuscarCollaboratorPorId(Guid id)
        {
            var busca = await _repo.BuscarCollaboratorPorId(id);
            if (busca == null) { throw new Exception("Collaborator não existe");}
            
            return new CollaboratorDto 
            { 
              Id = busca.Id,
              Name = busca.Name,
              ApplicationUser = busca.ApplicationUser,
              ApplicationUserId = busca.ApplicationUserId
            };
        }

        public async Task<TaskEntityDto> BuscarTarefaPorId(Guid id)
        {

            var tarefaBuscada = await _repo.BuscarTarefaPorId(id);
            if (tarefaBuscada == null)
            {
                throw new Exception("Tarefa buscada não foi encontrada.");
            }

            return new TaskEntityDto
            {
                Name = tarefaBuscada.Name,
                Description = tarefaBuscada.Description,
                CreatedAt = tarefaBuscada.CreatedAt,
                CollaboratorName = tarefaBuscada.Collaborator?.Name,
                ProjectName = tarefaBuscada.Project.Name,
                Id = tarefaBuscada.Id,
            };
        }


        public async Task<TaskEntityDto> EditarTarefa(Guid id, TaskEditDto dto)
        {
            var result = _editValidator.Validate(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var entity = new TaskEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                Project = dto.Project,
                ProjectId = dto.Project.Id,
                CollaboratorId = dto.CollaboratorId,
                TimeTrackers = dto.TimeTrackers,
                CreatedAt = dto.CreatedAt
            };
            var tarefaEditada = await _repo.EditarTarefa(id, entity);

            return new TaskEntityDto
            {
                Id = tarefaEditada.Id,
                Name = tarefaEditada.Name,
                Description = tarefaEditada.Description,
                ProjectName = tarefaEditada.Project.Name,
                CreatedAt = tarefaEditada.CreatedAt,
                UpdatedAt = tarefaEditada.UpdatedAt
            };
        }
        public async Task<List<TaskEntityDto>> FiltrarListaPorColaborador(TaskEntityDto dto,
            CollaboratorDto CollaboratorDto)
        {
            var busca = await _repo.ListarTarefas();
            if (busca == null)
            {
                throw new Exception("Não existem tarefas para serem listadas.");
            }
            if (CollaboratorDto == null)
            {
                throw new Exception("Não existem colaboradores para ser filtrados.");
            }

            var listaDto = busca
                .Where(x => x.CollaboratorId == CollaboratorDto.Id)
                 .Select(x => new TaskEntityDto
                 {
                     Name = x.Name,
                     Id = x.Id,
                     Description = x.Description,
                     ProjectName = x.Project.Name,
                     CollaboratorName = x.Collaborator != null ? x.Collaborator.Name : null,
                     CreatedAt = x.CreatedAt
                 })
                 .ToList();
            await SaveChangesAsync();
            return listaDto;
        }

        public async Task<List<TaskEntityDto>> FiltrarPorProjeto(Guid id)
        {
            var busca = await _repo.ListarTarefas();
            if (busca == null)
            {
                throw new Exception("Não existem tarefas a ser listadas.");
            }

            var listaDto = busca
                .Where(x => x.ProjectId == id)
                .Select(x => new TaskEntityDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                    ProjectName = x.Project.Name,
                    CollaboratorName = x.Collaborator != null ? x.Collaborator.Name : null,
                    CreatedAt = x.CreatedAt
                })
            .ToList();
            await SaveChangesAsync();
            return listaDto;  
        }

        public async Task<List<TaskEntityDto>> ListarTarefas()
        {
            var busca = await _repo.ListarTarefas();
            if (busca == null)
            {
                throw new Exception("Não existem tarefas para serem listadas.");
            }
            var listaDto = busca
                .Select(x => new TaskEntityDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                    ProjectName = x.Project.Name,
                })
            .ToList();
            await SaveChangesAsync();
            return listaDto;
        }


        public async Task<TaskEntityDto> RemoverTarefa(Guid id)
        {
            var busca = await _repo.BuscarTarefaPorId(id);
            if (busca == null)
            {
                throw new Exception("A tarefa não existe.");
            }

            var tarefaRemovida = await _repo.RemoverTarefa(id);

            return new TaskEntityDto
            {
                Name = tarefaRemovida.Name,
                Description = tarefaRemovida.Description,
                ProjectName = tarefaRemovida.Project.Name,
                CreatedAt = DateTime.UtcNow,
                CollaboratorName = tarefaRemovida.Collaborator?.Name
            };
        }

        public async Task<ProjectDto> BuscarProjetoPorId(Guid id)
        {
            var busca = await _repo.BuscarProjetoPorId(id);
            if (busca == null)
            {
                throw new Exception("Projeto não existe.");
            }

            return new ProjectDto
            {
                Id = busca.Id,
                Name = busca.Name,
                CreatedAt = busca.CreatedAt,
                UpdatedAt = busca.UpdatedAt,
                Tasks = busca.Tasks
            };
        }


        public async Task SaveChangesAsync()
        {
          await _repo.SaveChangesAsync();
        }

    }
}
        
using Api_IngaTasks.Application.Dtos.Collaborator;
using Api_IngaTasks.Application.Dtos.Project;
using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<TaskEntityDto> AdicionarTask(TaskCreateDto dto);
        public Task<List<TaskEntityDto>> ListarTarefas();
        public Task<TaskEntityDto> RemoverTarefa(Guid id);
        public Task<TaskEntityDto> BuscarTarefaPorId(Guid id);
        public Task<TaskEntityDto> EditarTarefa(Guid id, TaskEditDto dto);
        public Task<List<TaskEntityDto>> FiltrarPorProjeto(Guid id);
        public Task<List<TaskEntityDto>> FiltrarListaPorColaborador(TaskEntityDto dto,
            CollaboratorDto collaboratorDto);
        public Task AssociarTarefaAColaborador(TaskEntityDto dto, CollaboratorDto CollaboratorDto);
        public Task AssociarTarefaAoProjeto(TaskEntityDto dto, ProjectDto projectDto);
        public Task<CollaboratorDto> BuscarCollaboratorPorId(Guid id);
        public Task<ProjectDto> BuscarProjetoPorId(Guid id);
        public Task SaveChangesAsync();
    }
}

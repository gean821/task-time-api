using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Interfaces
{
    public interface ITasksRepository
    {
        public Task<TaskEntity> AdicionarTask(TaskEntity Task);
        public Task<List<TaskEntity>> ListarTarefas();
        public Task<TaskEntity> RemoverTarefa(Guid id);
        public Task<TaskEntity> BuscarTarefaPorId(Guid id );
        public Task<TaskEntity> EditarTarefa(Guid id, TaskEntity tarefaEditada);
        public Task <List<TaskEntity>> FiltrarPorProjeto(Guid id);
        public Task<List<TaskEntity>> FiltrarListaPorColaborador(Task task, Collaborator collaborator);
        public  Task AssociarTarefaAColaborador(TaskEntity task, Collaborator Collaborator);
        public Task AssociarTarefaAoProjeto(TaskEntity Task, Project Project);
        public Task<Collaborator> BuscarCollaboratorPorId(Guid id);
        public Task<Project> BuscarProjetoPorId(Guid id);
        public Task SaveChangesAsync();
    }
}

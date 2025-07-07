using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Configuracao;
using Api_IngaTasks.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _db;

        public TasksRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TaskEntity> AdicionarTask(TaskEntity task)
        {
            await _db.Tasks.AddAsync(task);
            await SaveChangesAsync();
            return task;
        }

        public async Task AssociarTarefaAColaborador(TaskEntity task, Collaborator collaborator)
        {
            var tarefaExistente = await _db.Tasks.FindAsync(task.Id);
            if (tarefaExistente == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            tarefaExistente.CollaboratorId = collaborator.Id;
            tarefaExistente.Collaborator = collaborator;
            await SaveChangesAsync();
        }

        public async Task AssociarTarefaAoProjeto(TaskEntity Task, Project Project)
        {
            var tarefaExistente = await _db.Tasks.FindAsync(Task.Id);
            if (tarefaExistente == null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            tarefaExistente.ProjectId = Project.Id;
            tarefaExistente.Project = Project;
            await SaveChangesAsync();
        }

        public async Task<Collaborator> BuscarCollaboratorPorId(Guid id)
        {
            var busca = await _db.Collaborators.FindAsync(id);
            if (busca == null) { throw new Exception("ID não existe."); }
            return busca;
        }

        public async Task<TaskEntity> BuscarTarefaPorId(Guid id)
            => await _db.Tasks
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
           
        public async Task<TaskEntity> EditarTarefa(Guid id, TaskEntity TarefaEditada)
        {
            var tarefa =  await _db.Tasks.FindAsync(id);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }
            tarefa.Project = TarefaEditada.Project;
            tarefa.Name = TarefaEditada.Name;
            tarefa.ProjectId = TarefaEditada.ProjectId;  
            tarefa.Description = TarefaEditada.Description;
            tarefa.Collaborator = TarefaEditada.Collaborator;
            tarefa.CollaboratorId = TarefaEditada.CollaboratorId;
            tarefa.CreatedAt = TarefaEditada.CreatedAt;
            tarefa.UpdatedAt = DateTime.UtcNow;
            tarefa.DeletedAt = TarefaEditada.DeletedAt;
            await _db.SaveChangesAsync();
            return tarefa;
        }

        public async Task<List<TaskEntity>> FiltrarListaPorColaborador(Task task, Collaborator collaborator)
        {
            var busca = await _db.Tasks
                .Include(c => c.Collaborator)
                .Where(c => c.CollaboratorId == collaborator.Id)
                .ToListAsync();
            return busca;
        }

        public async Task<List<TaskEntity>> FiltrarPorProjeto(Guid projectId)
        {
            var busca = await _db.Tasks
                .Include(c => c.Project)
                .Where(c => c.ProjectId == projectId )
                .ToListAsync();
            await SaveChangesAsync();
            return busca;
        }

        public Task<List<TaskEntity>> ListarTarefas()
        {
            var listaDeTarefas = _db.Tasks
                .ToListAsync();
            return listaDeTarefas;
        }
        public async Task<TaskEntity> RemoverTarefa(Guid id)
        {
            var tarefaRemovida = await BuscarTarefaPorId(id);
            if (tarefaRemovida == null)
            {
                throw new Exception("Tarefa não encontrada para ser removida.");
            }
            _db.Tasks.Remove(tarefaRemovida);
            await SaveChangesAsync();
            return tarefaRemovida;
        }

        public async Task<Project> BuscarProjetoPorId(Guid id)
        {
            var busca = await _db.Projects.FindAsync(id);
            if (busca == null)
            {
                throw new Exception("Projeto não encontrado.");
            }
            return busca;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

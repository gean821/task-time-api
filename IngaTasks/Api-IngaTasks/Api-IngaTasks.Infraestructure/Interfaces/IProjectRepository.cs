using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Interfaces
{
    public interface IProjectRepository
    {
        public Task<List<Project>> ListarProjetos();
        public Task <Project> RemoverProjeto(Guid id);
        public Task<Project> AdicionarProjeto(Project projectAdicionado);
        public Task <Project> EditarProjeto(Guid id,Project ProjectEditado);
        public Project ObterProjetoPorId(Guid id);
        Task SaveChangesAsync();
    }
}

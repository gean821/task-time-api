using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services.Interfaces
{
    public interface IProjectService 
    {
        public Task<List<Project>> ListarProjetos();
        public Task<Project> RemoverProjeto(Guid id);
        public Task AdicionarProjeto(Project project);
        public Task<Project> EditarProjeto(Guid id, Project ProjetoEditado);
        public Project ObterProjetoPorId(Guid id);
    }
}

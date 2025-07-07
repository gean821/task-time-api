using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Services.Interfaces;

namespace Api_IngaTasks.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository projectRepository)
        {
            _repo = projectRepository;
        }

        public async Task AdicionarProjeto(Project project)
        {
            await _repo.AdicionarProjeto(project);
            await _repo.SaveChangesAsync();
        }

        public async Task<Project> EditarProjeto(Guid id, Project project)
        {
            var projetoAtualizado = await _repo.EditarProjeto(id, project);
            if (projetoAtualizado == null)
            {
                throw new Exception("O projeto desejado para editar não existe.");
            }
            await _repo.SaveChangesAsync();
            return projetoAtualizado;
        }

        public Task<List<Project>> ListarProjetos()
        {
            var listaDeProjetos = _repo.ListarProjetos();
            if (listaDeProjetos == null)
            {
                throw new Exception("Não há nenhum projeto para ser listado.");
            }
            return listaDeProjetos;
        }
        public Project ObterProjetoPorId(Guid id)
        {
            var projetoBuscado = _repo.ObterProjetoPorId(id);
            if (projetoBuscado == null)
            {
                throw new Exception("O projeto buscado não existe.");
            }
            return projetoBuscado;
        }

        public Task<Project> RemoverProjeto(Guid id)
        {
            var projetoRemovido = _repo.RemoverProjeto(id);
            if (projetoRemovido == null)
            {
                throw new Exception("Projeto buscado para ser removido não existe.");
            }
            return projetoRemovido;   
        }
    }
}

     
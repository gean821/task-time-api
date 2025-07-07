using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Configuracao;
using Api_IngaTasks.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Repository
{

    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db) => _db = db;

        public async Task<Project> AdicionarProjeto(Project projectAdicionado)
        {
            await _db.Projects.AddAsync(projectAdicionado);
            await SaveChangesAsync();
            return projectAdicionado;
        }

        public async Task<Project> EditarProjeto(Guid id, Project projetoEditado)
        {
            var projetoBuscado = await _db.Projects.FindAsync(id);
            if (projetoBuscado == null)
            {
                throw new Exception("Projeto inexistente.");
            }
            projetoBuscado.Name = projetoEditado.Name;
            projetoBuscado.Id = projetoEditado.Id;
            projetoBuscado.Tasks = projetoEditado.Tasks;
            projetoBuscado.DeletedAt = projetoEditado.DeletedAt;
            projetoBuscado.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return projetoBuscado;
        }

        public async Task<List<Project>>? ListarProjetos()
        {
            var listaDeProjetos = await _db.Projects.ToListAsync();
            return listaDeProjetos;
        }
        public Project ObterProjetoPorId(Guid id)
            => _db.Projects
            .Where(a => a.Id == id)
            .FirstOrDefault();
      

        public async Task<Project> RemoverProjeto(Guid id)
        {
            var busca = ObterProjetoPorId(id);
            _db.Projects.Remove(busca);
            await SaveChangesAsync();
            return busca; 
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
       
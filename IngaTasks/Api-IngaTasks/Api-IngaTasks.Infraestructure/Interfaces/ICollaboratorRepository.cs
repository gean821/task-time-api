using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Interfaces
{
    public interface ICollaboratorRepository
    {
        public Task<List<Collaborator>> ListarColaboradores();
        public Task<Collaborator> AdicionarColaborador(Collaborator collaborator);
        public Task<Collaborator> RemoverColaborador(Guid id);
        public Task<Collaborator> EditarColaborador(Guid id, Collaborator collaborator);
        public Task SaveChangesAsync();
    }
}

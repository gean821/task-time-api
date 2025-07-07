using Api_IngaTasks.Application.Dtos.Collaborator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services.Interfaces
{
    public interface ICollaboratorService
    {
        public Task<CollaboratorDto> AdicionarColaborador(CollaboratorCreateDto dto);
        public Task<CollaboratorDto> RemoverColaborador(Guid id);
        public Task<CollaboratorDto> EditarColaborador(Guid id,CollaboratorEditDto dto);
        public Task<List<CollaboratorDto>> ListarColaboradores();
    }
}

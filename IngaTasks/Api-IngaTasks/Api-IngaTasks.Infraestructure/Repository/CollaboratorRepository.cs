using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Configuracao;
using Api_IngaTasks.Infraestructure.Interfaces;
using System.Data.Entity;

public class CollaboratorRepository : ICollaboratorRepository
{

    private readonly AppDbContext _db;

    public CollaboratorRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Collaborator> AdicionarColaborador(Collaborator collaborator)
    {
        await _db.Collaborators.AddAsync(collaborator);
        await SaveChangesAsync();
        return collaborator;
    }

    public async Task<Collaborator> EditarColaborador(Guid id, Collaborator collaborator)
    {
        var busca = await _db.Collaborators.FindAsync(id);
        if (busca == null)
        {
            throw new Exception("Não existe esse colaborador.");
        }

        busca.Name = collaborator.Name;
        busca.ApplicationUser = collaborator.ApplicationUser;
        busca.UpdatedAt = collaborator.UpdatedAt;
        busca.ApplicationUserId = collaborator.ApplicationUserId;
        busca.CreatedAt = collaborator.CreatedAt;
        busca.DeletedAt = collaborator.DeletedAt;
        await SaveChangesAsync();
        return busca;
    }


    public async Task<List<Collaborator>> ListarColaboradores()
    {
         return await _db.Collaborators.ToListAsync();
    }
        
    public async Task<Collaborator> RemoverColaborador(Guid id)
    {
        var busca = await _db.Collaborators.FindAsync(id);
        _db.Collaborators.Remove(busca);
        await SaveChangesAsync();
        return busca;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}


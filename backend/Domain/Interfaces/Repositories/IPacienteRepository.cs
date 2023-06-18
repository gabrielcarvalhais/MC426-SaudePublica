using MC426_Domain.Entities;

namespace MC426_Backend.Domain.Interfaces.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Paciente GetByChave(Guid chave);
    }
}

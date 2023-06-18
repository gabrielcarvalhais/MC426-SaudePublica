using MC426_Domain.Entities;

namespace MC426_Backend.Domain.Interfaces.Services
{
    public interface IPacienteService : IService<Paciente>
    {
        Paciente GetByChave(Guid chave);
    }
}

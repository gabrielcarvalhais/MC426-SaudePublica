using MC426_Backend.Domain.Entities;

namespace MC426_Backend.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Funcionario GetByChave(Guid chave);
    }
}

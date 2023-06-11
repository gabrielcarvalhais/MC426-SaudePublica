using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Infrastructure.Data;

namespace MC426_Backend.Infrastructure.Repositories
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(SaudePublicaContext context)
            : base(context)
        {

        }

        public override IQueryable<Funcionario> GetAll()
        {
            return base.GetAll()
                .Where(x => !x.Excluido);
        }

        public override void Delete(Funcionario obj)
        {
            obj.Excluido = true;
            base.Update(obj);
        }
    }
}

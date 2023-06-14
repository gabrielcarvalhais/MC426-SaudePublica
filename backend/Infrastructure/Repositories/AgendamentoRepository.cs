using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Domain.Entities;
using MC426_Infrastructure.Data;

namespace MC426_Backend.Infrastructure.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(SaudePublicaContext context)
            : base(context)
        {

        }

        public override IQueryable<Agendamento> GetAll()
        {
            return base.GetAll()
                .Where(x => !x.Excluido);
        }

        public override void Delete(Agendamento obj)
        {
            obj.Excluido = true;
            base.Update(obj);
        }
    }
}

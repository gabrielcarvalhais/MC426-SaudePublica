using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Domain.Entities;
using MC426_Infrastructure.Data;

namespace MC426_Backend.Infrastructure.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(SaudePublicaContext context)
            : base(context)
        {

        }

        public override IQueryable<Paciente> GetAll()
        {
            return base.GetAll()
                .Where(x => !x.Excluido);
        }

        public override void Delete(Paciente obj)
        {
            obj.Excluido = true;
            base.Update(obj);
        }
    }
}

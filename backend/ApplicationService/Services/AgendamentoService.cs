using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Backend.Domain.Interfaces.Services;

namespace MC426_Backend.ApplicationService.Services
{
    public class AgendamentoService : Service<Agendamento>, IAgendamentoService
    {
        protected IAgendamentoRepository _agendamentoRepository;

        public AgendamentoService(IRepository<Agendamento> repository,
            IAgendamentoRepository agendamentoRepository)
            : base(repository)
        {
            _repository = repository;
            _agendamentoRepository = agendamentoRepository;
        }

        public override IQueryable<Agendamento> GetAll()
        {
            return _agendamentoRepository.GetAll();
        }

        public override Agendamento GetById(int id)
        {
            return _agendamentoRepository.GetById(id);
        }

        public override void Insert(Agendamento obj)
        {
            _agendamentoRepository.Insert(obj);
        }

        public override void Update(Agendamento obj)
        {

            _agendamentoRepository.Update(obj);
        }

        public override void Delete(Agendamento obj)
        {
            _agendamentoRepository.Delete(obj);
        }
    }
}

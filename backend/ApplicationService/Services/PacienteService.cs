using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Domain.Entities;

namespace MC426_Backend.ApplicationService.Services
{
    public class PacienteService : Service<Paciente>, IPacienteService
    {
        protected IPacienteRepository _pacienteRepository;

        public PacienteService(IRepository<Paciente> repository,
            IPacienteRepository pacienteRepository)
            : base(repository)
        {
            _repository = repository;
            _pacienteRepository = pacienteRepository;
        }

        public override IQueryable<Paciente> GetAll()
        {
            return _pacienteRepository.GetAll();
        }

        public override Paciente GetById(int id)
        {
            return _pacienteRepository.GetById(id);
        }

        public override void Insert(Paciente obj)
        {
            _pacienteRepository.Insert(obj);
        }

        public override void Update(Paciente obj)
        {

            _pacienteRepository.Update(obj);
        }

        public override void Delete(Paciente obj)
        {
            _pacienteRepository.Delete(obj);
        }

        public Paciente GetByChave(Guid chave)
        {
            return _pacienteRepository.GetByChave(chave);
        }
    }
}

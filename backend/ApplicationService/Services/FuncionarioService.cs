using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Backend.Domain.Interfaces.Services;

namespace MC426_Backend.ApplicationService.Services
{
    public class FuncionarioService : Service<Funcionario>, IFuncionarioService
    {
        protected IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IRepository<Funcionario> repository,
            IFuncionarioRepository funcionarioRepository)
            : base(repository)
        {
            _repository = repository;
            _funcionarioRepository = funcionarioRepository;
        }

        public override IQueryable<Funcionario> GetAll()
        {
            return _funcionarioRepository.GetAll();
        }

        public override Funcionario GetById(int id)
        {
            return _funcionarioRepository.GetById(id);
        }

        public Funcionario GetByChave(Guid chave)
        {
            return _funcionarioRepository.GetByChave(chave);
        }

        public override void Insert(Funcionario obj)
        {
            _funcionarioRepository.Insert(obj);
        }

        public override void Update(Funcionario obj)
        {

            _funcionarioRepository.Update(obj);
        }

        public override void Delete(Funcionario obj)
        {
            _funcionarioRepository.Delete(obj);
        }
    }
}

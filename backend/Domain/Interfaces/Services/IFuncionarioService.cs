﻿using MC426_Backend.Domain.Entities;

namespace MC426_Backend.Domain.Interfaces.Services
{
    public interface IFuncionarioService : IService<Funcionario>
    {
        Funcionario GetByChave(Guid chave);
    }
}

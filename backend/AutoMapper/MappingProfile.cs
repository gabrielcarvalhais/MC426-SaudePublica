using AutoMapper;
using MC426_Backend.Domain.Entities;
using MC426_Backend.Models;
using MC426_Domain.Entities;

namespace MC426_Backend.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Paciente, PacienteModel>().ReverseMap();

            CreateMap<Funcionario, FuncionarioModel>().ReverseMap();

            CreateMap<Agendamento, AgendamentoModel>().ReverseMap();
        }
    }
}

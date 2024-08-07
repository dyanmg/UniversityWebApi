using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(x => x.Name, src => src.MapFrom(x => $"{x.FirstMidName} {x.LastName}"));
            CreateMap<StudentParamDTO, Student>();
        }
    }
}

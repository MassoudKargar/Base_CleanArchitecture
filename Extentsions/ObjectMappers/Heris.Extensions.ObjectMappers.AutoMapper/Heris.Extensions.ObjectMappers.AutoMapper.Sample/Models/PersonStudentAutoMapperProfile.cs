using AutoMapper;

namespace Heris.Extensions.ObjectMappers.AutoMapper.Sample.Models;

public class PersonStudentAutoMapperProfile : Profile
{
    public PersonStudentAutoMapperProfile()
    {
        CreateMap<Person, Student>().ReverseMap();
    }
}

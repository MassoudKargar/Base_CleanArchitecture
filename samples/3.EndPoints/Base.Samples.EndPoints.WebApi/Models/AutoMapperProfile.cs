using AutoMapper;
using Base.Samples.Core.Contracts.People;

using Base.Samples.Core.Domain.People.Entities;
namespace Base.Samples.EndPoints.WebApi.Moduls;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
    }
}

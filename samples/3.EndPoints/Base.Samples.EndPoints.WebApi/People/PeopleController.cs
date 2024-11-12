﻿using Base.Core.Contracts.Data;
using Base.Samples.Core.Contracts.People;
using Base.Samples.Core.Domain.People.Entities;

namespace Base.Samples.EndPoints.WebApi.People;

public class PeopleController : BaseGenericController<Person, long, PersonDto>
{
    public PeopleController(IGenericRepository<Person, long> repository) : base(repository)
    {
    }
}
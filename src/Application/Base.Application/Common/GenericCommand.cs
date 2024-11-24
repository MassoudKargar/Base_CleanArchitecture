﻿using MediatR;

using Microsoft.AspNetCore.OData.Query;

namespace Base.Application.Common;

public class GenericCommand<TId, TViewModel, TResponse>(TId id, TViewModel model, GenericAction genericAction) : IRequest<TResponse>
    where TId : struct
{
    public TId Id { get; } = id;
    public TViewModel Model { get; } = model;
    public GenericAction GenericActionData { get; } = genericAction;
}


public class GenericQuery<TId, TQuery, TResponse>(TId id, ODataQueryOptions<TQuery>? queryOptions, GenericAction genericAction) : IRequest<TResponse>
    where TId : struct
{
    public TId Id { get; } = id;
    public ODataQueryOptions<TQuery>? QueryOptions { get; } = queryOptions;
    public GenericAction GenericActionData { get; } = genericAction;
}
public enum GenericAction : byte
{
    GetAll = 0,
    GetById = 1,
    Insert = 2,
    Update = 3,
    Delete = 4,
}
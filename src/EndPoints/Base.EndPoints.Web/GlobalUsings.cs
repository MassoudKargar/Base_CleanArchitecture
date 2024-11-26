// Global using directives

global using System.Data;
global using Base.Core.Contracts.Data;
global using Base.Core.Domains.Entities;
global using Base.EndPoints.Web.Extensions;
global using Base.EndPoints.Web.ModelBinding;
global using Base.Extensions.DependencyInjection.Abstractions;
global using Base.Utility;
global using Base.Utility.Exceptions;

global using FluentValidation;
global using FluentValidation.AspNetCore;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyModel;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Primitives;
global using Microsoft.IdentityModel.Tokens;

global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using Ardalis.Result;
global using Ardalis.Result.AspNetCore;
global using AutoMapper;
global using Base.Application.BaseMediatR;
global using Base.Application.Common;
global using Base.EndPoints.Web.Attributes;
global using Base.Extensions.DependencyInjection;
global using Base.Utility.Extensions;
global using MediatR;
global using MediatR.Extensions.FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.OData.Query;

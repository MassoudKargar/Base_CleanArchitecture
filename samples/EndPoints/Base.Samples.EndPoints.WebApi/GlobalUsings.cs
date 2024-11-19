// Global using directives

global using System.Threading.RateLimiting;
global using AutoMapper;

global using Base.Core.Contracts.Data;
global using Base.EndPoints.Web.Controllers;
global using Base.EndPoints.Web.Extensions.DependencyInjection;
global using Base.EndPoints.Web.Middlewares;
global using Base.Extensions.DependencyInjection;
global using Base.Infrastructure.SqlContext;
global using Base.Sample.Application.People.Validators;
global using Base.Sample.Application.People.ViewModels;
global using Base.Samples.Core.Domain.People.Entities;
global using Base.Samples.EndPoints.WebApi.Extensions.DependencyInjection.Swaggers.Extensions;
global using Base.Samples.EndPoints.WebApi.Extensions.DependencyInjection.Swaggers.Filters;
global using Base.Samples.EndPoints.WebApi.Extensions.DependencyInjection.Swaggers.Options;
global using Base.Samples.Infrastructure.Common;
global using Microsoft.AspNetCore.Cors.Infrastructure;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.OpenApi.Models;


global using Swashbuckle.AspNetCore.SwaggerGen;
global using Swashbuckle.AspNetCore.SwaggerUI;
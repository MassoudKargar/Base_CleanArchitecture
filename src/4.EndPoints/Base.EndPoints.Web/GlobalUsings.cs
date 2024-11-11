// Global using directives

global using Base.Core.Contracts.ApplicationServices.Commands;
global using Base.Core.Contracts.ApplicationServices.Queries;
global using Base.Core.Contracts.Data.Commands;
global using Base.Core.Contracts.Data.Queries;
global using Base.EndPoints.Web.Middlewares.ApiExceptionHandler;
global using Base.Utility;

global using FluentValidation.AspNetCore;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Data.SqlClient;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using System.Net;
global using System.Reflection;
global using System.Security.Claims;
global using Base.Core.ApplicationServices.Commands;
global using Base.Core.ApplicationServices.Queries;
global using Base.Core.RequestResponse.Commands;
global using Base.Core.RequestResponse.Common;
global using Base.Core.RequestResponse.Queries;
global using Base.EndPoints.Web.Extensions;
global using Base.EndPoints.Web.ModelBinding;
global using Base.Extensions.DependencyInjection.Abstractions;
global using FluentValidation;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
global using Base.Extensions.Serializers.Abstractions;
global using Base.Extensions.Translations.Abstractions;
global using Microsoft.Extensions.DependencyModel;
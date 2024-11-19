﻿//using Base.Utility.Profiles;

//using System.ComponentModel.DataAnnotations;

//namespace Crypto.Application.DTOs.Common;
//public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
//        where TDto : class, new()
//        where TEntity : class, IEntity<TKey>, new()
//        where TKey : struct
//{
//    public TKey Id { get; set; }

//    public TEntity ToEntity(IMapper mapper)
//    {
//        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
//    }

//    public TEntity ToEntity(IMapper mapper, TEntity entity)
//    {
//        return mapper.Map(CastToDerivedClass(mapper, this), entity);
//    }

//    public static TDto FromEntity(IMapper mapper, TEntity model)
//    {
//        return mapper.Map<TDto>(model);
//    }

//    protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey> baseInstance)
//    {
//        return mapper.Map<TDto>(baseInstance);
//    }

//    public void CreateMappings(Profile profile)
//    {
//        profile.AllowNullCollections = true;
//        var mappingExpression = profile.CreateMap<TDto, TEntity>();

//        var dtoType = typeof(TDto);
//        var entityType = typeof(TEntity);
//        foreach (var property in entityType.GetProperties(System.Reflection.BindingFlags.Static))
//        {
//            var p = dtoType.GetProperty(property.Name);
//            if (p == null)
//                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
//        }

//        CustomMappings(mappingExpression.ReverseMap());
//    }

//    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
//    {
//    }
//}

//public abstract class BaseDto<TDto, TEntity, TKey, TGeneralStatus> : IHaveCustomMapping
//    where TDto : class, new()
//    where TEntity : class, IEntity<TKey, TGeneralStatus>, new()
//    where TKey : struct
//    where TGeneralStatus : struct
//{
//    [Display(Name = "ردیف")]
//    public TKey Id { get; set; }
//    public TGeneralStatus GeneralStatusId { get; set; }

//    public TEntity ToEntity(IMapper mapper)
//    {
//        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
//    }

//    public TEntity ToEntity(IMapper mapper, TEntity entity)
//    {
//        return mapper.Map(CastToDerivedClass(mapper, this), entity);
//    }

//    public static TDto FromEntity(IMapper mapper, TEntity model)
//    {
//        return mapper.Map<TDto>(model);
//    }

//    protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey, TGeneralStatus> baseInstance)
//    {
//        return mapper.Map<TDto>(baseInstance);
//    }

//    public void CreateMappings(Profile profile)
//    {
//        profile.AllowNullCollections = true;
//        var mappingExpression = profile.CreateMap<TDto, TEntity>();

//        var dtoType = typeof(TDto);
//        var entityType = typeof(TEntity);
//        //Ignore any property of source (like Post.Author) that dose not contains in destination 
//        foreach (var property in entityType.GetProperties(System.Reflection.BindingFlags.Static))
//        {
//            var p = dtoType.GetProperty(property.Name);
//            if (p == null)
//                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
//        }

//        CustomMappings(mappingExpression.ReverseMap());
//    }

//    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
//    {
//    }
//}


//public abstract class BaseDto<TDto, TEntity> : IHaveCustomMapping
//    where TDto : class, new()
//    where TEntity : class, IEntity, new()

//{
//    public TEntity ToEntity(IMapper mapper)
//    {
//        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
//    }

//    public TEntity ToEntity(IMapper mapper, TEntity entity)
//    {
//        return mapper.Map(CastToDerivedClass(mapper, this), entity);
//    }

//    public static TDto FromEntity(IMapper mapper, TEntity model)
//    {
//        return mapper.Map<TDto>(model);
//    }

//    protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity> baseInstance)
//    {
//        return mapper.Map<TDto>(baseInstance);
//    }

//    public void CreateMappings(Profile profile)
//    {
//        profile.AllowNullCollections = true;
//        var mappingExpression = profile.CreateMap<TDto, TEntity>();

//        var dtoType = typeof(TDto);
//        var entityType = typeof(TEntity);
//        foreach (var property in entityType.GetProperties(System.Reflection.BindingFlags.Static))
//        {
//            var p = dtoType.GetProperty(property.Name);
//            if (p == null)
//                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
//        }

//        CustomMappings(mappingExpression.ReverseMap());
//    }

//    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
//    {
//    }
//}
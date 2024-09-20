﻿using Ardalis.SmartEnum;
using CraftersCloud.Core.SmartEnums.Entities;
using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public abstract class EntityWithEnumIdConfiguration<TEntity, TId>()
    : CraftersCloud.Core.SmartEnums.EntityFramework.EntityWithEnumIdConfiguration<TEntity, TId>(NameMaxLength)
    where TEntity : EntityWithEnumId<TId>
    where TId : SmartEnum<TId>
{
    private const int NameMaxLength = 200;
}
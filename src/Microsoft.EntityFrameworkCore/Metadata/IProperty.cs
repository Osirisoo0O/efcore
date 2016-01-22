// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     Represents a scalar property of an entity.
    /// </summary>
    public interface IProperty : IPropertyBase
    {
        /// <summary>
        ///     Gets the type of value that this property holds.
        /// </summary>
        Type ClrType { get; }

        /// <summary>
        ///     Gets a value indicating whether this property can contain null.
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        ///     Gets a value indicating whether or not this property can be modified before the entity is
        ///     saved to the database. If true, an exception will be thrown if a value is assigned to
        ///     this property when it is in the <see cref="EntityState.Added" /> state.
        /// </summary>
        bool IsReadOnlyBeforeSave { get; }

        /// <summary>
        ///     Gets a value indicating whether or not this property can be modified after the entity is
        ///     saved to the database. If true, an exception will be thrown if a new value is assigned to
        ///     this property after the entity exists in the database.
        /// </summary>
        bool IsReadOnlyAfterSave { get; }

        /// <summary>
        ///     Gets a value indicating whether or not the database will always generate a value for this property.
        ///     If set to true, a value will always be read back from the database whenever the entity is saved
        ///     regardless of the state of the property. If set to false, whenever a value is assigned to the property
        ///     (or marked as modified) EF will attempt to save that value to the database rather than letting the
        ///     database generate one.
        /// </summary>
        bool IsStoreGeneratedAlways { get; }

        /// <summary>
        ///     Gets a value indicating when a value for this property will be generated by the database. Even when the
        ///     property is set to be generated by the database, EF may still attempt to save a specific value (rather than
        ///     having one generated by the database) when the entity is added and a value is assigned, or the property is
        ///     marked as modified for an existing entity. See <see cref="IsStoreGeneratedAlways" /> for more information.
        /// </summary>
        ValueGenerated ValueGenerated { get; }

        /// <summary>
        ///     Gets a value indicating whether this property requires a <see cref="ValueGenerator" /> to generate
        ///     values when new entities are added to the context.
        /// </summary>
        bool RequiresValueGenerator { get; }

        /// <summary>
        ///     Gets a value indicating whether this is a shadow property. A shadow property is one that does not have a
        ///     corresponding property in the entity class. The current value for the property is stored in
        ///     the <see cref="ChangeTracker" /> rather than being stored in instances of the entity class.
        /// </summary>
        bool IsShadowProperty { get; }

        /// <summary>
        ///     Gets a value indicating whether this property is used as a concurrency token. When a property is configured
        ///     as a concurrency token the value in the database will be checked when an instance of this entity type
        ///     is updated or deleted during <see cref="DbContext.SaveChanges()" /> to ensure it has not changed since
        ///     the instance was retrieved from the database. If it has changed, an exception will be thrown and the
        ///     changes will not be applied to the database.
        /// </summary>
        bool IsConcurrencyToken { get; }
    }
}

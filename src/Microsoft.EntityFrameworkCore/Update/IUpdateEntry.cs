// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Update
{
    /// <summary>
    ///     <para>
    ///         The information passed to a database provider to save changes to an entity to the database.
    ///     </para>
    ///     <para>
    ///         This interface is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public interface IUpdateEntry
    {
        /// <summary>
        ///     The type of entity to be saved to the database.
        /// </summary>
        IEntityType EntityType { get; }

        /// <summary>
        ///     The state of the entity to be saved.
        /// </summary>
        EntityState EntityState { get; }

        /// <summary>
        ///     Gets a value indicating if the specified property is modified. If true, the current value assigned
        ///     to the property should be saved to the database.
        /// </summary>
        /// <param name="property"> The property to be checked. </param>
        /// <returns> True if the property is modified, otherwise false. </returns>
        bool IsModified([NotNull] IProperty property);

        /// <summary>
        ///     Gets a value indicating if the specified property should have a value generated by the database.
        /// </summary>
        /// <param name="property"> The property to be checked. </param>
        /// <returns> True if the property should have a value generated by the database, otherwise false. </returns>
        bool IsStoreGenerated([NotNull] IProperty property);

        /// <summary>
        ///     Gets the value assigned to the property.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetCurrentValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property when it was retrieved from the database.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <returns> The value for the property. </returns>
        object GetOriginalValue([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property.
        /// </summary>
        /// <param name="propertyBase"> The property to get the value for. </param>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <returns> The value for the property. </returns>
        TProperty GetCurrentValue<TProperty>([NotNull] IPropertyBase propertyBase);

        /// <summary>
        ///     Gets the value assigned to the property when it was retrieved from the database.
        /// </summary>
        /// <param name="property"> The property to get the value for. </param>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <returns> The value for the property. </returns>
        TProperty GetOriginalValue<TProperty>([NotNull] IProperty property);

        /// <summary>
        ///     Gets the value assigned to the property.
        /// </summary>
        /// <param name="propertyBase"> The property to set the value for. </param>
        /// <param name="value"> The value to set. </param>
        void SetCurrentValue([NotNull] IPropertyBase propertyBase, [CanBeNull] object value);

        /// <summary>
        ///     Gets an <see cref="EntityEntry" /> for the entity being saved. <see cref="EntityEntry" /> is an API optimized for
        ///     application developers and <see cref="IUpdateEntry" /> is optimized for database providers, but there may be instances
        ///     where a database provider wants to access information from <see cref="EntityEntry" />.
        /// </summary>
        /// <returns> An <see cref="EntityEntry" /> for this entity. </returns>
        EntityEntry ToEntityEntry();
    }
}

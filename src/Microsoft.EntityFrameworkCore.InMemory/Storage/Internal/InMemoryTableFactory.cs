// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class InMemoryTableFactory : IdentityMapFactoryFactoryBase, IInMemoryTableFactory
    {
        private readonly ConcurrentDictionary<IKey, Func<IInMemoryTable>> _factories
            = new ConcurrentDictionary<IKey, Func<IInMemoryTable>>();

        public virtual IInMemoryTable Create(IEntityType entityType)
            => _factories.GetOrAdd(entityType.FindPrimaryKey(), Create)();

        private Func<IInMemoryTable> Create([NotNull] IKey key)
            => (Func<IInMemoryTable>)typeof(InMemoryTableFactory).GetTypeInfo()
                .GetDeclaredMethod(nameof(CreateFactory))
                .MakeGenericMethod(GetKeyType(key))
                .Invoke(null, new object[] { key });

        [UsedImplicitly]
        private static Func<IInMemoryTable> CreateFactory<TKey>(IKey key)
            => () => new InMemoryTable<TKey>(key.GetPrincipalKeyValueFactory<TKey>());
    }
}

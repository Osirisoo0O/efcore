// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.ValueGeneration.Internal
{
    public class SqlServerSequenceHiLoValueGenerator<TValue> : HiLoValueGenerator<TValue>
    {
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        private readonly ISqlServerUpdateSqlGenerator _sqlGenerator;
        private readonly ISqlServerConnection _connection;
        private readonly ISequence _sequence;

        public SqlServerSequenceHiLoValueGenerator(
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] ISqlServerUpdateSqlGenerator sqlGenerator,
            [NotNull] SqlServerSequenceValueGeneratorState generatorState,
            [NotNull] ISqlServerConnection connection)
            : base(generatorState)
        {
            Check.NotNull(rawSqlCommandBuilder, nameof(rawSqlCommandBuilder));
            Check.NotNull(sqlGenerator, nameof(sqlGenerator));
            Check.NotNull(connection, nameof(connection));

            _sequence = generatorState.Sequence;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _sqlGenerator = sqlGenerator;
            _connection = connection;
        }

        protected override long GetNewLowValue()
            => (long)Convert.ChangeType(
                _rawSqlCommandBuilder
                    .Build(_sqlGenerator.GenerateNextSequenceValueOperation(_sequence.Name, _sequence.Schema))
                    .ExecuteScalar(_connection),
                typeof(long),
                CultureInfo.InvariantCulture);

        public override bool GeneratesTemporaryValues => false;
    }
}

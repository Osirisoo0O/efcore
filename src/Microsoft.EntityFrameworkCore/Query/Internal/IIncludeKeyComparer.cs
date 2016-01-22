// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    public interface IIncludeKeyComparer
    {
        bool ShouldInclude(ValueBuffer valueBuffer);
    }
}

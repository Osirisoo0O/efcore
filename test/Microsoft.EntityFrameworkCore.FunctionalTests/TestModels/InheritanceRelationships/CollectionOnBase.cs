// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.InheritanceRelationships
{
    public class CollectionOnBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public BaseInheritanceRelationshipEntity Parent { get; set; }
    }
}

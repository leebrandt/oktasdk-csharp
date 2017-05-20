﻿using FluentAssertions;
using NSubstitute;
using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Okta.Sdk.UnitTests
{
    public class AbstractResourceShould
    {
        public class DummyResource : Resource
        {
            public DummyResource(IReadOnlyDictionary<string, object> data, IResourceFactory resourceFactory)
                : base(data, resourceFactory)
            { }

            public DummyResource Nested 
                => GetProperty<DummyResource>("foobar");
        }

        [Fact]
        public void NotThrowForNullData()
        {
            var resource = new DummyResource(null, Substitute.For<IResourceFactory>());
            resource.Should().NotBeNull();
        }

        [Fact]
        public void NotThrowForNonexistentNestedProperty()
        {
            var resource = new DummyResource(null, new DefaultResourceFactory());
            resource.Nested.Should().NotBeNull();
        }
    }
}

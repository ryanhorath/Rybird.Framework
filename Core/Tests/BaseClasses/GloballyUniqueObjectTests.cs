using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class GloballyUniqueObjectTests
    {
        [Fact]
        public void TestEquality()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var test1 = new TestGloballyUniqueObject(guid1);
            var test2 = new TestGloballyUniqueObject(guid1);
            var test3 = new TestGloballyUniqueObject(guid2);
            Assert.Equal(test1, test2);
            Assert.NotEqual(test1, test3);
            Assert.NotEqual(test2, test3);
        }
    }

    public class TestGloballyUniqueObject : GloballyUniqueObject
    {
        public TestGloballyUniqueObject() : base() {}

        public TestGloballyUniqueObject(Guid guid)
            : base(guid)
        {

        }
    }
}

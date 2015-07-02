using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class UniqueObjectTests
    {
        [Fact]
        public void TestEquality()
        {
            var test1 = new TestUniqueObject(1);
            var test2 = new TestUniqueObject(1);
            var test3 = new TestUniqueObject(2);
            Assert.Equal(test1, test2);
            Assert.NotEqual(test1, test3);
            Assert.NotEqual(test2, test3);
        }
    }

    public class TestUniqueObject : UniqueObject
    {
        public TestUniqueObject() : base() {}

        public TestUniqueObject(int uniqueId) : base(uniqueId)
        {

        }
    }
}

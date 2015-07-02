using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class EnumDescriptionAttributeTests
    {
        [Fact]
        public void TestSetProperty()
        {
            var test = new EnumDescriptionAttribute("ResourceTest");
            Assert.Equal("ResourceTest", test.DescriptionResourceName);
            Assert.Equal("ResourceTest", test.Description);
            EnumDescriptionAttribute.SetResourcesProvider(new TestResourcesProvider());
            Assert.Equal("ResourceTest", test.DescriptionResourceName);
            Assert.Equal("ResourceTestTesting", test.Description);
        }

        private class TestResourcesProvider : IResourcesProvider
        {
            public string GetString(string resourceId)
            {
                return resourceId + "Testing";
            }
        }
    }
}

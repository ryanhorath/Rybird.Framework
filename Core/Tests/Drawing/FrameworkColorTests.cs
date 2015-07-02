using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.ObjectModel;

namespace Rybird.Framework.Core.Tests
{
    public class FrameworkColorTests
    {
        [Fact]
        public void TestHueSaturationBrightness()
        {
            var color1 = FrameworkColor.FromArgb(0, 74, 127);
            var brightness = color1.GetBrightness();
            Assert.True(color1.GetBrightness() == 100f);
        }
    }
}

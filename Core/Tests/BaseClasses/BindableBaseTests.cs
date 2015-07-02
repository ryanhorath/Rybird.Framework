using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class BindableBaseTests
    {
        private int _timesCalled = 0;

        [Fact]
        public void TestSetProperty()
        {
            var test = new TestBindableBase();
            test.TestProperty = "";
            test.PropertyChanged += test_PropertyChanged;
            test.TestProperty = "";
            Assert.True(_timesCalled == 0);
            test.TestProperty = "Test";
            Assert.True(_timesCalled == 1);
            Assert.Equal(test.TestProperty, "Test");
            test.PropertyChanged -= test_PropertyChanged;
        }

        private void test_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _timesCalled++;
        }

        public class TestBindableBase : BindableBase
        {
            private string _testProperty;
            public string TestProperty
            {
                get { return _testProperty; }
                set { SetProperty<string>(ref _testProperty, value); }
            }
        }
    }
}

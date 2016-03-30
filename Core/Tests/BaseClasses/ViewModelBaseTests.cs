using System.Collections;
using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class ViewModelBaseTests
    {
        private int _timesCalled = 0;

        [Fact]
        public void TestSetProperty()
        {
            //var test = new TestViewModelBase();
            //test.TestProperty = "";
            //test.PropertyChanged += test_PropertyChanged;
            //test.TestProperty = "";
            //Assert.True(_timesCalled == 0);
            //test.TestProperty = "Test";
            //Assert.True(_timesCalled == 1);
            //Assert.Equal(test.TestProperty, "Test");
            //test.PropertyChanged -= test_PropertyChanged;
            //Assert.False(test.HasErrors);
            //var errors = test.GetErrors("TestProperty");
            //Assert.True(errors == null || ((IList)errors).Count == 0);
            //test.TestProperty = null;
            //errors = test.GetErrors("TestProperty");
            //Assert.True(test.HasErrors);
            //Assert.True(((IList)errors).Count == 1);
        }

        private void test_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TestProperty")
            {
                _timesCalled++;
            }
        }
    }
}

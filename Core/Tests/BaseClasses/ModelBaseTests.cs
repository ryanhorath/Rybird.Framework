using Xunit;

namespace Rybird.Framework.Core.Tests
{
    public class ModelBaseTests
    {
        private int _timesCalled = 0;

        [Fact]
        public void TestSetProperty()
        {
            //var test = new TestModelBase();
            //test.TestProperty = "Test";
            //test.PropertyChanged += test_PropertyChanged;
            //test.TestProperty = "Test";
            //Assert.True(_timesCalled == 0);
            //test.TestProperty = "Test2";
            //Assert.True(_timesCalled == 1);
            //Assert.Equal(test.TestProperty, "Test2");
            //test.PropertyChanged -= test_PropertyChanged;
            //var exception = Assert.Throws<ValidationException>(() => test.TestProperty = null);
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

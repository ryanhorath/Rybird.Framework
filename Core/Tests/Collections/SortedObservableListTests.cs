using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.ObjectModel;

namespace Rybird.Framework.Core.Tests
{
    public class SortedObservableListTests
    {
        [Fact]
        public void TestConstruction()
        {
            var list = new List<int>() { 1, 3, 0 };
            var sortedList = new SortedObservableList<int>(list);
            Assert.Equal(list.Count, sortedList.Count);
            Assert.Equal(sortedList[0], 0);
            Assert.Equal(sortedList[1], 1);
            Assert.Equal(sortedList[2], 3);
        }

        [Fact]
        public void TestAdd()
        {
            var sortedList = new SortedObservableList<int>();
            sortedList.Add(1);
            sortedList.AddNearBeginning(3);
            sortedList.AddNearEnd(0);
            sortedList.Add(2);
            Assert.Equal(0, sortedList[0]);
            Assert.Equal(1, sortedList[1]);
            Assert.Equal(2, sortedList[2]);
            Assert.Equal(3, sortedList[3]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.ObjectModel;

namespace Rybird.Framework.Core.Tests
{
    public class CollectionSyncerTests
    {
        [Fact]
        public void TestConstruction()
        {
            var badList = new List<string>();
            var sourceList = new ObservableList<string>();
            var targetList = new Collection<string>();
            Assert.Throws<ArgumentNullException>(() => new CollectionSyncer<string, string>(null, targetList, (x) => x, (x) => x, false));
            Assert.Throws<ArgumentNullException>(() => new CollectionSyncer<string, string>(sourceList, null, (x) => x, (x) => x, false));
            Assert.Throws<Exception>(() => new CollectionSyncer<string, string>(badList, targetList, (x) => x, (x) => x, false));

            sourceList.Add("Test1");
            sourceList.Add("Test2");
            var syncer1 = new CollectionSyncer<string, string>(sourceList, targetList, (x) => x, (x) => x, false);
            Assert.Equal(2, sourceList.Count);
            Assert.Equal(0, targetList.Count);

            var syncer2 = new CollectionSyncer<string, string>(sourceList, targetList, (x) => x, (x) => x, true);
            Assert.Equal(sourceList.Count, targetList.Count);
        }

        [Fact]
        public void TestAddRemoveDispose()
        {
            var sourceList = new ObservableList<string>();
            var targetList = new Collection<string>();
            var syncer1 = new CollectionSyncer<string, string>(sourceList, targetList, (x) => x.ToUpper(), (x) => x.ToUpper(), false);
            var syncer2 = new CollectionSyncer<string, string>(sourceList, targetList, (x) => x.ToLower(), (x) => x.ToLower(), false);
            sourceList.Add("Test1");
            Assert.Equal(2, targetList.Count);
            Assert.True(targetList.Contains("test1"));
            Assert.True(targetList.Contains("TEST1"));
            sourceList.Remove("Test1");
            Assert.Equal(sourceList.Count, targetList.Count);
            Assert.Equal(0, targetList.Count);
            syncer2.Dispose();
            sourceList.Add("Test1");
            Assert.Equal(sourceList.Count, targetList.Count);
        }
    }
}

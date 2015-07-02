using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.ObjectModel;

namespace Rybird.Framework.Core.Tests
{
    public class DelegateCommandTests
    {
        [Fact]
        public async Task TestConstructionAndExecute()
        {
            bool actioned1 = false;
            var command1 = new DelegateCommand<string>((s) => actioned1 = true);
            await command1.Execute(null);
            Assert.True(actioned1);
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<string>((s) => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<string>(null, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<string>(null));
            bool actioned2 = false;
            var command2 = new DelegateCommand<string>((s) =>
                {
                    if (s == "Test")
                    {
                        actioned2 = true;
                    }
                });
            await command2.Execute("Test");
            Assert.True(actioned2);

            bool actioned3 = false;
            var command3 = new DelegateCommand<string>((s) => actioned3 = true);
            await command3.Execute(null);
            Assert.True(actioned3);
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(() => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null));
        }

        [Fact]
        public void TestCanExecuteChanged()
        {
            bool actioned1 = false;
            var command1 = new DelegateCommand<string>((s) => { });
            command1.CanExecuteChanged += (s, e) => actioned1 = true;
            command1.RaiseCanExecuteChanged();
            Assert.True(actioned1);
        }

        [Fact]
        public async Task TestConstructionAndExecute2()
        {
            bool actioned1 = false;
            var command1 = new DelegateCommand(() => actioned1 = true);
            await command1.Execute();
            Assert.True(actioned1);
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(() => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null));
            bool actioned2 = false;
            var command2 = new DelegateCommand(() =>
            {
                actioned2 = true;
            });
            await command2.Execute();
            Assert.True(actioned2);

            bool actioned3 = false;
            var command3 = new DelegateCommand(() => actioned3 = true);
            await command3.Execute();
            Assert.True(actioned3);
        }

        [Fact]
        public void TestCanExecuteChanged2()
        {
            bool actioned1 = false;
            var command1 = new DelegateCommand(() => { });
            command1.CanExecuteChanged += (s, e) => actioned1 = true;
            command1.RaiseCanExecuteChanged();
            Assert.True(actioned1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework.Testing
{
    internal class TestModelBase : ModelBase
    {
        private string _testProperty;
        [Required]
        public string TestProperty
        {
            get { return _testProperty; }
            set { SetProperty<string>(ref _testProperty, value); }
        }
    }
}

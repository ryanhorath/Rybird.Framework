using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.ComponentModel;

namespace Rybird.Framework
{
    internal class RadioGroupBinding<TSource> : BindableBase, IDisposable
    {
        private bool _disposed = false;
        private IList<RadioButton> _targets;
        private PropertyBinding<int, TSource> _propertyBinding;
        private bool _updatingInProgress = false;

        public RadioGroupBinding(IEnumerable<RadioButton> targets, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, int> converter)
        {
            _targets = new List<RadioButton>(targets);
            _propertyBinding = new PropertyBinding<int, TSource>(this, "SelectedIndex", sourceObject, sourceProperty, converter);
            
            foreach (var target in _targets)
            {
                target.CheckedChange += target_CheckedChange;
            }
        }

        private void target_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (!_updatingInProgress)
            {
                var radioButton = sender as RadioButton;
                // Only change for newly checked box, ignore newly unchecked box
                if (radioButton.Checked)
                {
                    SelectedIndex = _targets.IndexOf(radioButton);
                }
            }
        } 

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _updatingInProgress = true;
                SetProperty<int>(ref _selectedIndex, value);
                for (int i = 0; i < _targets.Count; i++)
                {
                    if (i == _selectedIndex)
                    {
                        _targets[i].Checked = true;
                    }
                    else
                    {
                        _targets[i].Checked = false;
                    }
                }
                _updatingInProgress = false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (var target in _targets)
                    {
                        target.CheckedChange -= target_CheckedChange;
                    }
                    _targets.Clear();
                    _propertyBinding.Dispose();
                }
                _disposed = true;
            }
        }

    }
}
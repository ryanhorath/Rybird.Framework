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
using Java.Lang;

namespace Rybird.Framework
{
    public class ObservableAdapterFilter<T> : Filter
    {
        private readonly ObservableAdapter<T> _adapter;
        private readonly Func<string, IEnumerable<T>> _filter;

        public ObservableAdapterFilter(ObservableAdapter<T> adapter, Func<string, IEnumerable<T>> filter)
        {
            adapter.ThrowIfNull("adapter");
            filter.ThrowIfNull("filter");
            _adapter = adapter;
            _filter = filter;
        }

        private string _currentFilter = "";
        internal string CurrrentFilter
        {
            get { return _currentFilter; }
        }
 
        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var filterResults = new FilterResults();
            var filter = constraint.ToString();
            _currentFilter = filter;
            var results = _filter.Invoke(filter);
            // Nasty piece of .NET to Java wrapping, be careful with this!
            filterResults.Values = FromArray(results
                .Select(r => new JavaWrapper<T>(r))
                .ToArray());
            filterResults.Count = results.Count();
            constraint.Dispose();
            return filterResults;
        }
 
        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            using (var values = results.Values)
            {
                _adapter.FilteredItems = values.ToArray<Java.Lang.Object>()
                    .Select(r => r.ToNetObject<T>()).ToList();
            }
            _adapter.NotifyDataSetChanged();
            // Don't do this and see GREF counts rising
            constraint.Dispose();
            results.Dispose();
        }
    }
}
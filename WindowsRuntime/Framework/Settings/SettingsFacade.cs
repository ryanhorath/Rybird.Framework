using Windows.Storage;

namespace Rybird.Framework
{
    public class SettingsFacade : ISettingsProvider
    {
        public void AddOrUpdate(string key, object value)
		{
			ApplicationData.Current.RoamingSettings.Values[key] = value;
		}

		public bool TryGetValue<T>(string key, out T value)
		{
			var result = false;
			if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(key))
			{
				value = (T)ApplicationData.Current.RoamingSettings.Values[key];
				result = true;
			}
			else
			{
				value = default(T);
			}

			return result;
		}

		public bool Remove(string key)
		{
			return ApplicationData.Current.RoamingSettings.Values.Remove(key);
		}

		public bool ContainsKey(string key)
		{
			return ApplicationData.Current.RoamingSettings.Values.ContainsKey(key);
		}
	}
}

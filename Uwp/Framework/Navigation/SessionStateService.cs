using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Rybird.Framework;
using Windows.ApplicationModel.Resources;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    public class SessionStateService : ISessionStateService
    {
        private Frame _frame;
        private Dictionary<string, Dictionary<string, object>> _sessionState = new Dictionary<string, Dictionary<string, object>>();
        private IList<Type> _knownTypes = new List<Type>();

        public SessionStateService(Frame frame)
        {
            _frame = frame;
        }

        public IDictionary<string, object> GetPageSessionState(string pageKey)
        {
            if (!string.IsNullOrEmpty(pageKey))
            {
                return _sessionState.GetValueOrAddDefault(pageKey, () => new Dictionary<string, object>());
            }
            else
            {
                throw new ArgumentException("Argument cannot be null or empty", "pageKey");
            }
        }

        public IDictionary<string, object> GetPageSessionState()
        {
            var key = GeneratePageKey();
            Dictionary<string, object> sessionState = null;
            if (_sessionState.ContainsKey(key))
            {
                sessionState = _sessionState[key];
            }
            else
            {
                sessionState = new Dictionary<string, object>();
                _sessionState.Add(key, sessionState);
            }
            return sessionState;
        }

        public void RemovePageSessionState()
        {
            var pageKey = GeneratePageKey();
            _sessionState.Remove(pageKey);
        }

        private string GeneratePageKey()
        {
            return "Page-" + _frame.BackStackDepth;
        }

        public void RegisterKnownType(Type type)
        {
            _knownTypes.Add(type);
        }

        public async Task SaveAsync()
        {
            try
            {
                SaveFrameNavigationState(_frame);

                // Serialize the session state synchronously to avoid asynchronous access to shared state
                MemoryStream sessionData = new MemoryStream();
                DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, Dictionary<string, object>>), _knownTypes);
                serializer.WriteObject(sessionData, _sessionState);

                // Get an output stream for the SessionState file and write the state asynchronously
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constants.SessionStateFileName, CreationCollisionOption.ReplaceExisting);
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    sessionData.Seek(0, SeekOrigin.Begin);
                    var provider = new DataProtectionProvider("LOCAL=user");

                    // Encrypt the session data and write it to disk.
                    await provider.ProtectStreamAsync(sessionData.AsInputStream(), fileStream);
                    await fileStream.FlushAsync();
                }
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        public async Task RestoreSessionStateAsync()
        {
            _sessionState = new Dictionary<string, Dictionary<String, Object>>();

            try
            {
                // Get the input stream for the SessionState file
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(Constants.SessionStateFileName);
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                {
                    var memoryStream = new MemoryStream();
                    var provider = new DataProtectionProvider("LOCAL=user");

                    // Decrypt the prevously saved session data.
                    await provider.UnprotectStreamAsync(inStream, memoryStream.AsOutputStream());
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    // Deserialize the Session State
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, Dictionary<string, object>>),
                                                                                   _knownTypes);
                    _sessionState = (Dictionary<string, Dictionary<string, object>>)serializer.ReadObject(memoryStream);
                }
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        public void RestoreFrameState()
        {
            try
            {
                RestoreFrameNavigationState(_frame);
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        private void RestoreFrameNavigationState(Frame frame)
        {
            if (_sessionState.ContainsKey("Navigation") && _sessionState["Navigation"].ContainsKey("NavigationState"))
            {
                frame.SetNavigationState((string)_sessionState["Navigation"]["NavigationState"]);
            }
        }

        private void SaveFrameNavigationState(Frame frame)
        {
            var stateDictionary = new Dictionary<string, object>();
            // It is not at all suspected, but by design frame.GetNavigationState() will call OnNavigatedFrom on the current page
            stateDictionary.Add("NavigationState", frame.GetNavigationState());
            _sessionState["Navigation"] = stateDictionary;
        }
    }

    /// <summary>
    /// The exception that is thrown when a session state service error is detected.
    /// </summary>
    public class SessionStateServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionStateServiceException"/> class.
        /// </summary>
        public SessionStateServiceException()
            : base((ResourceLoader.GetForViewIndependentUse(Constants.StoreAppsInfrastructureResourceMapId)).GetString("SessionStateServiceFailed"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionStateServiceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SessionStateServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionStateServiceException"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public SessionStateServiceException(Exception exception)
            : base((ResourceLoader.GetForViewIndependentUse(Constants.StoreAppsInfrastructureResourceMapId)).GetString("SessionStateServiceFailed"), exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionStateServiceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public SessionStateServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

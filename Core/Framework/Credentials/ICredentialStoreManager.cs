//using Windows.Security.Credentials;

namespace Rybird.Framework
{
    public interface ICredentialStoreManager
    {
        void SaveCredentials(string resource, string userName, string password);
        //PasswordCredential GetCredentials(string resource);
        void RemoveCredentials(string resource);
    }
}

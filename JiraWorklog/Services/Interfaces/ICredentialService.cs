namespace JiraWorklog.Services
{
    public interface ICredentialService
    {
        string GetBaseURL();
        string GetBasicAuthString();
        void StoreCredentials(string url, string username, string password);
        void ResetCredentials();
    }
}
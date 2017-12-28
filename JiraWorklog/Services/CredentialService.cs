using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Services
{
    public class CredentialService : ICredentialService
    {
        public void StoreCredentials(string url, string username, string password)
        {
            Properties.Settings.Default.JiraURL = url;

            byte[] buffer = new UTF8Encoding().GetBytes(username + ":" + password);
            Properties.Settings.Default.BasicAuth = Convert.ToBase64String(buffer);

            Properties.Settings.Default.Save();
        }

        public void ResetCredentials()
        {
            Properties.Settings.Default.BasicAuth = null;

            Properties.Settings.Default.Save();
        }

        public string GetBasicAuthString() => Properties.Settings.Default.BasicAuth;

        public string GetBaseURL() => Properties.Settings.Default.JiraURL;
    }
}

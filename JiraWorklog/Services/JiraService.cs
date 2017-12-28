using JiraWorklog.Helpers;
using JiraWorklog.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Services
{
    public class JiraService : IIssueTrackerService
    {
        private static Logger Logger => LogManager.GetCurrentClassLogger();

        private HttpClient _client;

        private readonly ICredentialService _credentialService;

        public JiraService(ICredentialService credentialService)
        {
            _credentialService = credentialService;
        }

        private void ResetCredentials()
        {
            _client?.Dispose();
            _client = null;
        }

        private void EnsureCredentials()
        {
            if (_client == null)
            {
                _client = new HttpClient
                {
                    MaxResponseContentBufferSize = 256000
                };
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            if (_client.DefaultRequestHeaders.Authorization == null)
            {   
                _client.BaseAddress = new Uri(_credentialService.GetBaseURL());
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _credentialService.GetBasicAuthString());
            };
        }

        private async Task<T> RequestJSON<T>(string restURL) where T : class
        {
            try
            {
                EnsureCredentials();

                var response = await _client.GetAsync(restURL);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(jsonString);

                    return await Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        private async Task<string> PostJSON(string restURL, HttpContent content)
        {
            try
            {
                EnsureCredentials();

                var response = await _client.PostAsync(restURL, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public async Task<IList<JiraIssue>> GetIssuesInProgress()
        {
            var result = await RequestJSON<JiraIssueCollection>($"rest/api/2/search?jql=assignee=currentUser()+AND+status+not+in+(\"To Do\",Done)&fields=issuetype,description,summary,worklog,project");

            foreach (var issue in result?.Issues)
            {
                var sorted = issue.WorklogCollection.Worklogs.OrderByDescending(x => x.Started).ToList();
                issue.WorklogCollection.Worklogs = sorted;
            }

            return await Task.FromResult(result.Issues);
        }

        public async Task<JiraIssue> UpdateIssue(string issueId) => await RequestJSON<JiraIssue>($"rest/api/2/issue/{issueId}?fields=description,summary,worklog");

        public async Task SaveWorklogAsync(JiraWorklogEntry log, string issueId)
        {
            var postBody = JsonConvert.SerializeObject(log,
                                                          Formatting.None,
                                                          new JsonSerializerSettings
                                                          {
                                                              NullValueHandling = NullValueHandling.Ignore,
                                                              DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                                                              DateFormatString = "yyy-MM-ddTHH:mm:ss.fff+0000"

                                                          });

            var content = new StringContent(postBody, Encoding.UTF8, "application/json");
            await PostJSON($"rest/api/2/issue/{issueId}/worklog", content);
        }

        public async Task<ConnectionResult> TryConnectAsync()
        {
            var result = new ConnectionResult() { Successfull = true };

            try
            {
                ResetCredentials();
                EnsureCredentials();

                var response = await _client.GetAsync($"rest/api/2/mypermissions");
                if (!response.IsSuccessStatusCode)
                {
                    result.Successfull = false;
                    result.Message = response.ReasonPhrase;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result.Message = "Invalid Username/Password";
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        result.Message = "Url not found";
                    }
                }
            }
            catch (Exception e)
            {
                result.Successfull = false;
                result.Message = e.Message;
            }

            return await Task.FromResult(result);
        }
    }
}

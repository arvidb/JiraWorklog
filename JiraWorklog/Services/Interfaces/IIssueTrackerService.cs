using System.Collections.Generic;
using System.Threading.Tasks;
using JiraWorklog.Helpers;
using JiraWorklog.Models;

namespace JiraWorklog.Services
{
    public interface IIssueTrackerService
    {
        Task<IList<JiraIssue>> GetIssuesInProgress();
        Task SaveWorklogAsync(JiraWorklogEntry log, string issueId);
        Task<ConnectionResult> TryConnectAsync();
        Task<JiraIssue> UpdateIssue(string issueId);
    }
}
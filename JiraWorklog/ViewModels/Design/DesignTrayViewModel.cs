using JiraWorklog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.ViewModels.Design
{
    public class DesignTrayViewModel
    {
        public IEnumerable<JiraIssue> Issues => new List<JiraIssue>()
        {
            JsonConvert.DeserializeObject<JiraIssue>("{\"id\":\"10004\",\"self\":\"http://192.168.1.49:2990/jira/rest/api/2/issue/10004\",\"fields\":{\"summary\":\"wer\",\"description\":null,\"worklog\":{\"worklogs\":[{\"started\":\"2017-12-27T00:00:00+01:00\",\"timeSpent\":\"4h\",\"timeSpentSeconds\":14400,\"comment\":\"Test\"}],\"TotalLogged\":4.0,\"LastLogged\":\"2017-12-27T00:00:00+01:00\"},\"project\":{\"name\":\"abc\"}},\"Summary\":\"wer\",\"Description\":\"\",\"ProjectName\":\"abc\",\"WorklogCollection\":{\"worklogs\":[{\"started\":\"2017-12-27T00:00:00+01:00\",\"timeSpent\":\"4h\",\"timeSpentSeconds\":14400,\"comment\":\"Test\"}],\"TotalLogged\":4.0,\"LastLogged\":\"2017-12-27T00:00:00+01:00\"}}")
        };
    }
}

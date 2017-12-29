using JiraWorklog.Helpers;
using JiraWorklog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JiraWorklog.ViewModels.Design
{
    public class DesignTrayViewModel : ITrayPopupViewModel
    {
        public ObservableCollection<JiraIssue> Issues => new ObservableCollection<JiraIssue>()
        {
            JsonConvert.DeserializeObject<JiraIssue>("{\"id\":\"10004\",\"self\":\"http://sampleurl/jira/rest/api/2/issue/10004\",\"fields\":{\"summary\":\"Sample Issue\",\"description\":null,\"worklog\":{\"worklogs\":[{\"started\":\"2017-12-27T00:00:00+01:00\",\"timeSpent\":\"4h\",\"timeSpentSeconds\":14400,\"comment\":\"Test\"}],\"TotalLogged\":4.0,\"LastLogged\":\"2017-12-27T00:00:00+01:00\"},\"project\":{\"name\":\"Sample Project\"}},\"Summary\":\"Sample Issue\",\"Description\":\"\",\"ProjectName\":\"Sample Project\",\"WorklogCollection\":{\"worklogs\":[{\"started\":\"2017-12-27T00:00:00+01:00\",\"timeSpent\":\"4h\",\"timeSpentSeconds\":14400,\"comment\":\"Test\"}],\"TotalLogged\":4.0,\"LastLogged\":\"2017-12-27T00:00:00+01:00\"}}")
        };

        public ICommand ItemClickCommand => throw new NotImplementedException();

        public ICommand ReloadCommand => throw new NotImplementedException();
        
        public event EventHandler TrayItemExecuted { add { } remove { } }
    }
}

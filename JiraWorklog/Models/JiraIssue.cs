using JiraWorklog.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Models
{
    public class JiraIssueCollection
    {
        [JsonProperty("issues")]
        public IList<JiraIssue> Issues { get; set; }
    }

    public class JiraIssueType
    {
        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
    }

    public class FieldsInternal
    {
        [JsonProperty("issuetype")]
        public JiraIssueType IssueType { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("worklog")]
        public JiraWorklogCollection WorklogCollection { get; set; }

        [JsonProperty("project")]
        public JiraProject Project { get; set; }
    }

    public class JiraIssue : ObservableObject
    {
        private string _id;
        [JsonProperty("id")]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }



        private string _self;
        [JsonProperty("self")]
        public string Self
        {
            get => _self;
            set
            {
                _self = value;
                RaisePropertyChanged();
            }
        }


        private FieldsInternal _fields;
        [JsonProperty("fields")]
        public FieldsInternal Fields
        {
            get => _fields;
            set
            {
                _fields = value;
                RaisePropertyChanged();
            }
        }

        #region Helpers
        public string Summary => Fields.Summary ?? "";

        public string Description => Fields.Description ?? "";

        public string ProjectName => Fields.Project?.Name ?? "";

        public JiraWorklogCollection WorklogCollection => Fields.WorklogCollection;
        #endregion
    }
}

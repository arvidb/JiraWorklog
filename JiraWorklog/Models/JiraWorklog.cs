using JiraWorklog.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Models
{
    public class JiraWorklogCollection
    {
        [JsonProperty("worklogs")]
        public IList<JiraWorklogEntry> Worklogs { get; set; }

        public float TotalLogged => Worklogs?.DefaultIfEmpty().Sum(x => (x?.TimeSpentSeconds ?? 0.0f) / 3600.0f) ?? 0.0f;

        public DateTime? LastLogged => Worklogs?.DefaultIfEmpty().Max(x => x?.Started);
    }

    public class JiraWorklogEntry : ObservableObject
    {
        private DateTime _started;
        [JsonProperty("started")]
        public DateTime Started
        {
            get => _started;
            set
            {
                _started = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => StartedTimespanProxy);
            }
        }


        private string _timeSpent;
        [JsonProperty("timeSpent")]
        public string TimeSpent
        {
            get => _timeSpent;
            set
            {
                _timeSpent = value;
                RaisePropertyChanged();
            }
        }


        private ulong _timeSpentSeconds;
        [JsonProperty("timeSpentSeconds")]
        public ulong TimeSpentSeconds
        {
            get => _timeSpentSeconds;
            set
            {
                _timeSpentSeconds = value;
                RaisePropertyChanged();
            }
        }


        private string _comment;
        [JsonProperty("comment")]
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                RaisePropertyChanged();
            }
        }


        [JsonIgnore()]
        public TimeSpan StartedTimespanProxy
        {
            get
            {
                //Extract the timespan from the original datetime
                return Started - Started.Date;
            }
            set
            {
                //See if the timespan is different the the current value
                if (StartedTimespanProxy != value)
                {
                    //If it is, set the original date time to the
                    //original date, plus the new timespan value
                    Started = Started.Date.Add(value);

                    RaisePropertyChanged();
                }
            }
        }
    }
}

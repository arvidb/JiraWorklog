using System;

namespace JiraWorklog.Events
{
    public class RequestCloseDialogEventArgs : EventArgs
    {
        public bool DialogResult { get; set; }
        public RequestCloseDialogEventArgs(bool dialogresult) => DialogResult = dialogresult;
    }
}

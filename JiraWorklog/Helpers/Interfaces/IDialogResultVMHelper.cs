using JiraWorklog.Events;
using System;

namespace JiraWorklog.Common
{
    public interface IDialogResultVMHelper
    {
        event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
    }
}

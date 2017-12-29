using JiraWorklog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraWorklog.Events;
using JiraWorklog.Helpers;

namespace JiraWorklog.ViewModels
{
    public abstract class BaseDialogViewModel : ObservableObject, IDialogResultVMHelper
    {
        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;

        protected void CloseDialog(bool success) => this.RequestCloseDialog?.Invoke(this, new RequestCloseDialogEventArgs(success));

        protected void CloseDialog() => CloseDialog(true);

        protected void CancelDialog() => CloseDialog(false);
    }
}

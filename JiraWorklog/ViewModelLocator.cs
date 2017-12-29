using JiraWorklog.Helpers;
using JiraWorklog.ViewModels;
using JiraWorklog.ViewModels.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JiraWorklog
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => Bootstrapper.Resolve<MainViewModel>();

        public ITrayPopupViewModel TrayPopupViewModel => Bootstrapper.Resolve<ITrayPopupViewModel>();

        public NewWorklogViewModel NewWorklogViewModel => Bootstrapper.Resolve<NewWorklogViewModel>();
    }
}

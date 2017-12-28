using JiraWorklog.Common;
using JiraWorklog.Events;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JiraWorklog.Windows
{

    /// <summary>
    /// Interaction logic for WindowDialog.xaml
    /// </summary>
    public partial class WindowDialog : MetroWindow
    {
        // Note: If the window is closed, it has no DialogResult
        private bool _isClosed = false;

        private IDialogResultVMHelper _dialogVM;

        public WindowDialog()
        {
            InitializeComponent();
            
            this.DialogPresenter.DataContextChanged += DialogPresenterDataContextChanged;
            this.Closed += DialogWindowClosed;

            this.PreviewKeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    RequestCloseDialog(this, new RequestCloseDialogEventArgs(false));
                }
            };
        }

        void DialogWindowClosed(object sender, EventArgs e)
        {
            if (_dialogVM != null)
            {
                _dialogVM.RequestCloseDialog -= RequestCloseDialog;
            }

            _dialogVM = null;

            this._isClosed = true;
        }

        private void DialogPresenterDataContextChanged(object sender,
                                  DependencyPropertyChangedEventArgs e)
        {
            var d = e.NewValue as IDialogResultVMHelper;
            _dialogVM = d;
            if (d != null)
            {
                d.RequestCloseDialog += RequestCloseDialog;
            }
        }

        private void RequestCloseDialog(object sender, RequestCloseDialogEventArgs e)
        {
            if (_isClosed)
            {
                return;
            }

            this.DialogResult = e.DialogResult;
        }
    }
}

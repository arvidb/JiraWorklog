using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using NLog;
using JiraWorklog.Windows;

namespace JiraWorklog.Services
{
    public class WPFUIService : IUIService
    {
        private static Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>
        ///   A value indicating whether the UI is currently busy
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Sets the busystate as busy.
        /// </summary>
        public void SetBusyState() => SetBusyState(true);

        /// <summary>
        /// Sets the busystate to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private void SetBusyState(bool busy)
        {
            if (busy != _isBusy)
            {
                _isBusy = busy;
                Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                if (_isBusy)
                {
                    new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle, DispatcherTimer_Tick, System.Windows.Application.Current.Dispatcher);
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dispatcherTimer)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }
        
        private List<WindowDialog> _dialogWindows = new List<WindowDialog>();
        public bool? ShowModalDialog(string title, object datacontext, bool allowResize = true)
        {
            var win = new WindowDialog
            {
                Title = title,
                DataContext = datacontext,
                ResizeMode = allowResize ? ResizeMode.CanResize : ResizeMode.NoResize
            };

            _dialogWindows.Add(win);
            try
            {
                var result = win.ShowDialog();

                return result;
            }
            finally
            {
                _dialogWindows.Remove(win);
            }
        }
    }
}

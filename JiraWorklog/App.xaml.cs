using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JiraWorklog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger Logger => LogManager.GetCurrentClassLogger();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Logger.Info($"Starting JiraWorklog {version}" +
#if DEBUG
                    " Debug" +
#endif
                    $" - {Environment.OSVersion.VersionString}");

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            DispatcherUnhandledException += AppDispatcherUnhandledException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Logger.Info("Shutdown JiraWorklog");
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            CurrentDomainUnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, false));
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            CurrentDomainUnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, false));
        }


        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            CurrentDomainUnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, false));
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Logger.Fatal(ex);
            }
        }
    }
}

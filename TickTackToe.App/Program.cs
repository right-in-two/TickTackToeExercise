using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;
using TickTackToe.Interfaces;

namespace TickTackToe.App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var di = new DiConfig();
            var diProvider = di.BuildDiServiceProvider();
            var loggerFactory = diProvider.GetRequiredService<ILoggerFactory>();
            var mainLogger = loggerFactory.CreateLogger(nameof(Main));
            var boardFactory = diProvider.GetRequiredService<IBoardFactory>();

            Application.ThreadException += (sender, e) => Application_ThreadException(sender, e, mainLogger);            
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => CurrentDomain_UnhandledException(sender, e, mainLogger);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGame(boardFactory, loggerFactory.CreateLogger<FormGame>()));
        }
        
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e, ILogger logger)
        {
            Exception exception = null;
            if (e.ExceptionObject is Exception)
            {
                exception = e.ExceptionObject as Exception;
            }

            logger.LogCritical(exception, "UnhandledException");
            Environment.ExitCode = 1;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e, ILogger logger)
        {
            logger.LogCritical(e.Exception, "UnhandledException");
            Environment.ExitCode = 1;
        }
    }
}

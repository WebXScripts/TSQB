using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace TSQB
{
    internal class Core : IDisposable
    {

        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        
        public static async Task Main()
        {
            var core = new Core();
            AppDomain.CurrentDomain.UnhandledException += core.ExceptionHandler;
            await BotLoader.InitBot();
        }
        
        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.ExceptionObject as Exception, "Unhandled exception detected.");
            Logger.Fatal("Report this on: github.com/webxscripts/TeamSpeakCSharpBot");
            Dispose();
        }

        public void Dispose()
        {
            Logger.Info("TSQB is shutting down!");
            Environment.Exit(-1);
        }
    }
}
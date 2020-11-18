using System;
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
            while (!Console.KeyAvailable) await Task.Delay(TimeSpan.FromSeconds(0.1));
        }
        
        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal("Unhandled exception detected.");
            Logger.Fatal("Report this on: github.com/webxscripts/TeamSpeakCSharpBot");
            Dispose();
        }

        public void Dispose()
        {
            Console.WriteLine("TSQB is shutting down!");
            Environment.Exit(-1);
        }
    }
}
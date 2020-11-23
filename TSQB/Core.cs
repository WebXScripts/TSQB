using System;
using System.Threading.Tasks;
using CommandLine;
using NLog;
using TSQB.Models;

namespace TSQB
{
    internal class Core : IDisposable
    {

        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        
        public static async Task Main(string[] args)
        {
            var core = new Core();
            var config = Parser.Default.ParseArguments<ConnectionConfig>(args).WithNotParsed(
                x =>
                {
                    Logger.Error("Some params are missing!");
                    core.Dispose();
                });
            AppDomain.CurrentDomain.UnhandledException += core.ExceptionHandler;
            await BotLoader.InitBot(config.Value);
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
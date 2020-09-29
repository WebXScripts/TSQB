using System;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TSQB.Modules;

namespace TSQB
{
    internal class Core
    {
        private static TeamSpeakClient TsClient;
        
        static async Task Main()
        {
            Console.Title = "TSQB 1.0";
            TsClient = await BotModule.InitBot();
            EventsModule.EventHandler(TsClient);
            Console.WriteLine("Press any key to stop.");
            while (!Console.KeyAvailable) await Task.Delay(TimeSpan.FromSeconds(0.1));
        }

        public static void Dispose()
        {
            Console.WriteLine("TSQB is shutting down...");
            TsClient.Dispose();
            Environment.Exit(0);
        }
    }
}
using System;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;
using TSQB.Functions;
using TSQB.Models;

namespace TSQB.Modules
{
    public class EventsModule
    {
        private static readonly onClientJoin OnClientJoin = new onClientJoin();
        private static readonly ConnectionConfig Config = new ConfigModule().ReadConnectionConfig();
        
        public static void EventHandler(TeamSpeakClient TsClient)
        {
            Task.WaitAll(
                onClientJoin(TsClient),
                onCommand(TsClient)
                );
        }

        private static async Task onClientJoin(TeamSpeakClient TsClient)
        {
            TsClient.Subscribe<ClientEnterView>(async data =>
            {
                foreach (ClientEnterView client in data)
                {
                    foreach (var Function in Config.OnClientJoin)
                    {
                        OnClientJoin.GetType().GetMethod(Function)?.Invoke(OnClientJoin, new object[] { TsClient, client });
                    }
                }
            });
        }

        private static async Task onInterval(TeamSpeakClient TsClient)
        {
            //Interval implement.
        }

        private static async Task onCommand(TeamSpeakClient TsClient)
        {
            TsClient.Subscribe<TextMessage>(async data =>
            {
                foreach (var info in data)
                {
                    await CommandsModule.HandleCommand(TsClient, info.InvokerId, info.Message);
                }
            });
        }
    }
}
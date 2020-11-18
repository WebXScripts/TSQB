using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Events
{
    public static class EventsManager
    {
        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

        public static async Task HandleEvents(TeamSpeakClient tsClient)
        {
            try
            {
                await Task.WhenAll(
                    OnClientJoin(tsClient),
                    OnClientMoved(tsClient)
                );
            }
            catch (Exception e)
            {
                Logger.Fatal(e, "An error has been detected!");
            }
        }

        private static async Task OnClientJoin(TeamSpeakClient tsClient)
        {
            Logger.Info("OnClientJoin event loaded!");
            tsClient.Subscribe<ClientEnterView>(data => 
                data.ForEach(client =>
                    {
                        if (client != null)
                        {
                            ClientJoin.AvailableFunctions.ForEach(async func =>
                            {
                                await func(tsClient, client);
                            });
                        }
                    }
                ));
        }
        
        private static async Task OnClientMoved(TeamSpeakClient tsClient)
        {
            Logger.Info("OnClientMoved event loaded!");
            tsClient.Subscribe<ClientMoved>(data => 
                data.ForEach(client =>
                    {
                        if (client != null)
                        {
                            ClientChannelChanged.AvailableFunctions.ForEach(async func =>
                            {
                                await func(tsClient, client);
                            });
                        }
                    }
                ));
        }

        private static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var i in collection) action(i);
        }
    }
}
using System;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Events
{
    public static class EventsManager
    {
        public static void EventHandler(TeamSpeakClient tsClient)
        {
            try
            {
                Task.WaitAll(
                    OnClientJoin(tsClient)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has been detected: {e}!");
            }
        }

        private static async Task OnClientJoin(TeamSpeakClient tsClient)
        {
            tsClient.Subscribe<ClientEnterView>(async data =>
            {
                foreach (var client in data)
                {
                    Console.WriteLine($"{client.NickName} joined.");
                }
            });
        }

        private static async Task onInterval(TeamSpeakClient TsClient)
        {
            //Interval implement.
        }
    }
}
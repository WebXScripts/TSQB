using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Events
{
    
     // Explanation: These are TEST functions and their operation may differ from the end application

     public static class ClientChannelChanged
    {
        public static readonly List<Func<TeamSpeakClient, ClientMoved, Task>> AvailableFunctions =
            new List<Func<TeamSpeakClient, ClientMoved, Task>>
            {
                PokeAdmins
            };

        private static async Task PokeAdmins(TeamSpeakClient tsClient, ClientMoved client)
        {
            var channelId = 2;
            var groupToPoke = 6;
            var clients = await tsClient.GetClients();
            var toPoke = await tsClient.GetServerGroupClientList(groupToPoke);
            if (client.TargetChannel == channelId)
            {
                foreach (var clid in client.ClientIds)
                {
                    var clientInfo = await tsClient.GetClientInfo(clid);
                    await tsClient.SendMessage($"Witaj {clientInfo.NickName} na kanale pomocy!", MessageTarget.Private, clid);
                    await tsClient.SendMessage($"Za chwilę jakiś administrator udzieli Ci pomocy.", MessageTarget.Private, clid);
                    foreach (var admin in toPoke)
                    {
                        var seriuslyToPoke = clients.Where(c => c.DatabaseId == admin.ClientDatabaseId);
                        foreach (var oknow in seriuslyToPoke)
                        {
                            await tsClient.PokeClient(oknow.Id, "Ktos czeka na kanale pomocy!");
                        }
                    }
                }
            }
        }
    }
}
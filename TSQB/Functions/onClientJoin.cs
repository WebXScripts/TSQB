using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Functions
{
    public class onClientJoin
    {
        public static void Test(TeamSpeakClient TsClient, ClientEnterView Client)
        {
            TsClient.SendMessage("Witaj!", MessageTarget.Private, Client.Id);
            TsClient.SendMessage(Client.NickName, MessageTarget.Private, Client.Id);
        }

        public static void Test2(TeamSpeakClient TsClient, ClientEnterView Client)
        {
            TsClient.SendMessage($"Witaj! {Client.NickName}!", MessageTarget.Private, Client.Id);
            TsClient.SendMessage("Informacje o tobie:", MessageTarget.Private, Client.Id);
            TsClient.SendMessage($"UID: {Client.Uid}", MessageTarget.Private, Client.Id);
            TsClient.SendMessage($"Nick: {Client.NickName}", MessageTarget.Private, Client.Id);
            TsClient.SendMessage($"Dbid: {Client.DatabaseId}", MessageTarget.Private, Client.Id);
        }
    }
}
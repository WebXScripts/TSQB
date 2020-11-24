using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Commands
{
    public static class Commands
    {
        public static readonly Dictionary<string, Func<TeamSpeakClient, TextMessage, object[], Task>> AvailableFunctions =
            new Dictionary<string, Func<TeamSpeakClient, TextMessage, object[], Task>>
            {
                {"helloworld", HelloWorld}
            };
        
        private static async Task HelloWorld(TeamSpeakClient tsClient, TextMessage client, params object[] args)
        {
            if (args.Length != 0)
            {
                var arguments = String.Join(" ", args);
                await tsClient.SendMessage($"You said: {arguments}", MessageTarget.Private, client.InvokerId);
            }
            else
            {
                await tsClient.SendMessage("You said nothing! :(", MessageTarget.Private, client.InvokerId);
            }
        }
    }
}
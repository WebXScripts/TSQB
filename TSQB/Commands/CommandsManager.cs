using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Commands
{
    public static class CommandsManager
    {
        
        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

        public static async Task HandleCommands(TeamSpeakClient tsClient)
        {
            var commands = Commands.AvailableFunctions;
            Logger.Info("Commands have been loaded!");
            tsClient.Subscribe<TextMessage>(data => 
                data.ForEach(async client =>
                {
                    if (client != null)
                    {
                        if (client.InvokerUid == "serveradmin") return;
                        if (client.Message.StartsWith("!"))
                        {
                            var usedcommand = client.Message.Split(" ");
                            var commandName = usedcommand[0].Replace("!", "");
                            if (commands.ContainsKey(commandName))
                            {
                                var arguments = usedcommand.Skip(1).Where(x => !String.IsNullOrEmpty(x)).ToArray();
                                await commands[commandName](tsClient, client, arguments);
                            }
                            else
                            {
                                await tsClient.SendMessage("This command does not exists.",
                                    MessageTarget.Private,
                                    client.InvokerId);
                            }
                        }
                        else
                        {
                            await tsClient.SendMessage("Use a valid command starting with [b]`!`[/b].", MessageTarget.Private, client.InvokerId);
                        }
                    }
                }));
        }
        
        private static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var i in collection) action(i);
        }
    }
}
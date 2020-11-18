using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TSQB.Events
{
    public static class ClientJoin
    {
        
        // Explanation: These are TEST functions and their operation may differ from the end application
        
        public static readonly List<Func<TeamSpeakClient, ClientEnterView, Task>> AvailableFunctions =
            new List<Func<TeamSpeakClient, ClientEnterView, Task>>
            {
                WelcomeMessage
            };
        
        private static async Task WelcomeMessage(TeamSpeakClient tsClient, ClientEnterView client)
        {
            await tsClient.SendMessage($"Welcome {client.NickName}!", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Today is {DateTime.Today.ToString(CultureInfo.CurrentCulture)}!", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Your UID is {client.Uid}.", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Have a nice day!", MessageTarget.Private, client.Id);
            Process p = Process.GetCurrentProcess();
            double cpuload = p.TotalProcessorTime.TotalMilliseconds / 1000;
            var ramload = p.PrivateMemorySize64 / 1024 / 1024;
            await tsClient.SendMessage($"CPU: {cpuload}", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"RAM: {ramload}", MessageTarget.Private, client.Id);
        }
    }
}
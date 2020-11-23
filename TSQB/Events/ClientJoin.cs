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
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            await tsClient.SendMessage($"Welcome {client.NickName}!", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Today is {DateTime.Today.ToString(CultureInfo.CurrentCulture)}!", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Your UID is {client.Uid}.", MessageTarget.Private, client.Id);
            await tsClient.SendMessage($"Have a nice day!", MessageTarget.Private, client.Id);
            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            var result = cpuUsageTotal * 100;
            await tsClient.SendMessage($"CPU USAGE: {result.ToString()}", MessageTarget.Private, client.Id);
        }
    }
}
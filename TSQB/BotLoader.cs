using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using TeamSpeak3QueryApi.Net;
using TeamSpeak3QueryApi.Net.Specialized;
using TSQB.Events;
using TSQB.Models;

namespace TSQB
{
    public static class BotLoader
    {

        private static TeamSpeakClient _tsClient;
        private static readonly ConnectionConfig Config = JsonConvert.DeserializeObject<ConnectionConfig>
            (File.ReadAllText(Path.GetFullPath(@"Configs/Connection.json")));
        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        private static readonly string Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly() .Location).FileVersion;

        
        private static void Header()
        {
            Logger.Info("Welcome to TSQB");
            Logger.Info($"Version: {Version}");
            Logger.Info("Created by WebXScripts.ovh");
            Logger.Info( "Have a nice day!");
        }
        
        public static async Task InitBot()
        {
            Header();
            Logger.Warn("Loading TSQB...");
            _tsClient = new TeamSpeakClient(Config.Ip, Config.QueryPort);
            try
            {
                await _tsClient.Connect();
                await _tsClient.Login(Config.QueryLogin, Config.QueryPassword);
                await _tsClient.UseServer(Config.ServerId);
                await _tsClient.ChangeNickName(Config.QueryNickname);
                await _tsClient.RegisterServerNotification();
                await _tsClient.RegisterTextPrivateNotification();
                await _tsClient.RegisterChannelNotification(0);
                await EventsManager.HandleEvents(_tsClient);
                Logger.Info("Welcome abort captain, all systems online.");
                await KeepAlive();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "An error has been detected!");
                Environment.Exit(-1);
            }
        }
        
        private static async Task KeepAlive()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await _tsClient.WhoAmI();
                    await Task.Delay(30000);
                }
            });
        }
    }
}
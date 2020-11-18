using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using TeamSpeak3QueryApi.Net.Specialized;
using TSQB.Events;
using TSQB.Models;

namespace TSQB
{
    public static class BotLoader
    {

        private static TeamSpeakClient _tsClient;
        private static readonly ConnectionConfig Config = JsonConvert.DeserializeObject<ConnectionConfig>
            (File.ReadAllText(Path.GetFullPath(@"configs/connection.json")));
        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        
        private static void Header()
        {
            Logger.Info("Welcome to TSQB 1.1-a");
            Logger.Info("Created by WebXScripts.ovh");
            Logger.Info( "Have a nice day!");
        }
        
        public static async Task InitBot()
        {
            Header();
            Logger.Warn("Loading TSQB...");
            _tsClient = new TeamSpeakClient(Config.IP, Config.QueryPort);
            try
            {
                await _tsClient.Connect();
                await _tsClient.Login(Config.QueryLogin, Config.QueryPassword);
                await _tsClient.UseServer(Config.ServerID);
                await _tsClient.ChangeNickName(Config.QueryNickname);
                await _tsClient.RegisterServerNotification();
                await _tsClient.RegisterTextPrivateNotification();
                await _tsClient.RegisterChannelNotification(0);
                EventsManager.EventHandler(_tsClient);
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An error has been detected: {ex.Message}");
                Environment.Exit(-1);
            }

            Logger.Info("Welcome abort captain, all systems online.");
        }
    }
}
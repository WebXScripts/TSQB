using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using NLog;
using TeamSpeak3QueryApi.Net.Specialized;
using TSQB.Commands;
using TSQB.Events;
using TSQB.Models;

namespace TSQB
{
    public static class BotLoader
    {

        private static TeamSpeakClient _tsClient;
        private static readonly ILogger Logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        private static readonly string Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly() .Location).FileVersion;

        
        private static void Header()
        {
            Logger.Info("Welcome to TSQB");
            Logger.Info($"Version: {Version}");
            Logger.Info("Created by WebXScripts.ovh");
            Logger.Info( "Have a nice day!");
        }
        
        public static async Task InitBot(ConnectionConfig config)
        {
            Header();
            Logger.Warn("Loading TSQB...");
            _tsClient = new TeamSpeakClient(config.Ip, config.QueryPort);
            try
            {
                await _tsClient.Connect();
                await _tsClient.Login(config.QueryLogin, config.QueryPassword);
                await _tsClient.UseServer(config.ServerId);
                await _tsClient.ChangeNickName(config.QueryNickname);
                await _tsClient.RegisterServerNotification();
                await _tsClient.RegisterTextPrivateNotification();
                await _tsClient.RegisterChannelNotification(0);
                await EventsManager.HandleEvents(_tsClient);
                await CommandsManager.HandleCommands(_tsClient);
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
                    await Task.Delay(180000);
                }
            });
        }
    }
}
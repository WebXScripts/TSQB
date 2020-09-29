using System;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;
using TSQB.Models;

namespace TSQB.Modules
{
    public class BotModule
    {
        
        private static readonly ConnectionConfig Config = new ConfigModule().ReadConnectionConfig();

        public static async Task<TeamSpeakClient> InitBot()
        { 
            TeamSpeakClient TsClient = new TeamSpeakClient(Config.IP, Config.QueryPort);
            try
            {
                await TsClient.Connect();
                await TsClient.Login(Config.QueryLogin, Config.QueryPassword);
                await TsClient.UseServer(Config.ServerID);
                await TsClient.ChangeNickName(Config.QueryNickname);
                await TsClient.RegisterServerNotification();
                await TsClient.RegisterTextPrivateNotification();
                await TsClient.RegisterChannelNotification(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Core.Dispose();
            }
            return TsClient;
        }
    }
}
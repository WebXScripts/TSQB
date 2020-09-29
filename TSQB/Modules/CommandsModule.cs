using System;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net.Specialized;

namespace TSQB.Modules
{
    public class CommandsModule
    {
        /*
         * a chuj pozniej ide spac
         */
        
        public static async Task HandleCommand(TeamSpeakClient TsClient, int Id, string Message)
        {
            if (Id != TsClient.WhoAmI().Id)
            {
                var Client = await TsClient.GetClientInfo(Id);
                Console.WriteLine(Client.NickName);
                Console.WriteLine(Message);
            }
        }
    }
}
using CommandLine;

namespace TSQB.Models
{
    public class ConnectionConfig
    {
        [Option("ip", Default = "localhost", HelpText = "TeamSpeak IPv4 Address", Required = false)]
        public string Ip { get; set; }
        [Option("port", Default = 10011, HelpText = "TeamSpeak Query port (def. 10011)", Required = false)]
        public int QueryPort { get; set; }
        [Option("id", Default = 1, HelpText = "TeamSpeak ServerId", Required = false)]
        public int ServerId { get; set; }
        [Option("login", Default = "serveradmin", Required = false)]
        public string QueryLogin { get; set; }
        [Option("password", Default = "foobar", Required = true)]
        public string QueryPassword { get; set; }
        [Option("nickname", Default = "TSQB @ Bot", HelpText = "Bot name (def. TSQB @ Bot)", Required = false)]
        public string QueryNickname { get; set; }
    }
}
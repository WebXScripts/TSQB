namespace TSQB.Models
{
    public class ConnectionConfig
    {
        public string IP { get; set; }
        public int QueryPort { get; set; }
        public int ServerID { get; set; }
        public string QueryLogin { get; set; }
        public string QueryPassword { get; set; }
        public string QueryNickname { get; set; }
        public string[] OnClientJoin { get; set; }
    }
}
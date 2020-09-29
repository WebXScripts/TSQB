using System.IO;
using Newtonsoft.Json;
using TSQB.Models;

namespace TSQB.Modules
{
    public class ConfigModule
    {
        public ConnectionConfig ReadConnectionConfig()
            => JsonConvert.DeserializeObject<ConnectionConfig>
            (File.ReadAllText(Path.GetFullPath(@"configs/main.json")));
    }
}
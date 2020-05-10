using Newtonsoft.Json;
using RosSharp.RosBridgeClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosSharp.RosBridgeClient.Services.FileServer
{
    class GetFileRequest : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "file_server/get_file";
        public string name;
        public GetFileRequest(string name)
        {
            this.name = name;
        }


    }
    public class GetFileResponse : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "file_server/get_file";
        public byte[] value;
    }
}

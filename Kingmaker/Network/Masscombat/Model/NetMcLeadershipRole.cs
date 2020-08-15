using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class NetMcLeadershipRole : ISerializable
    {
        public int Id;
        public string Name;
        public bool MainDepartment;
        public int CommandId;

        public NetMcLeadershipRole()
        {
        }

        public NetMcLeadershipRole(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("id");
            Name = info.GetString("name");
            MainDepartment = info.GetBoolean("mainDepartment");
            CommandId = info.GetInt32("commandId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", Id);
            info.AddValue("name", Name);
            info.AddValue("mainDepartment", MainDepartment);
            info.AddValue("commandId", CommandId);
        }
    }
}

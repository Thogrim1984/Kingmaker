using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    class NetMcCommand : ISerializable
    {
        public int Id;
        public String Name;
        public int CommandVariable;
        public String Tooltip;
        public String Description;

        public NetMcCommand()
        {
        }

        public NetMcCommand(SerializationInfo info, StreamingContext context)
        {
            info.GetInt32("id");
            info.GetString("name");
            info.GetInt32("commandVariable");
            info.GetString("tooltip");
            info.GetString("description");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id",Id);
            info.AddValue("name", Name);
            info.AddValue("commandVariable", CommandVariable);
            info.AddValue("tooltip", Tooltip);
            info.AddValue("description", Description);
        }
    }
}

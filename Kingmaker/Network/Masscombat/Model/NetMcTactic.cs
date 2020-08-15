using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class  NetMcTactic : ISerializable
    {
        public int Id;
        public string Name;
        public string Description;
        public string Tooltip;

        public NetMcTactic()
        {
        }

        public NetMcTactic(SerializationInfo info, StreamingContext context)
        {
            info.GetInt32("id");
            info.GetString("name");
            info.GetString("description");
            info.GetString("tooltip");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", Id);
            info.AddValue("name", Name);
            info.AddValue("description", Description);
            info.AddValue("tooltip", Tooltip);
        }
    }
}

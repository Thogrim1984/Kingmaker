using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class NetMcType : ISerializable
    {
        public int Id;
        public string Name;
        public int Melee;
        public int Ranged;
        public int Defensive;
        public double Health;
        public double MovementStrategically;
        public int MovementTactical;
        public int Morale;
        public int Fortitude;
        public int Reflex;
        public double Value;
        public double Upkeep;

        public NetMcType()
        {
        }

        public NetMcType(SerializationInfo info, StreamingContext context)
        {
            info.GetInt32("id");
            info.GetString("name");
            info.GetInt32("melee");
            info.GetInt32("ranged");
            info.GetInt32("defensive");
            info.GetDouble("health");
            info.GetDouble("movementStrategically");
            info.GetInt32("movementTactical");
            info.GetInt32("morale");
            info.GetInt32("fortitude");
            info.GetInt32("reflex");
            info.GetDouble("value");
            info.GetDouble("upkeep");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", Id);
            info.AddValue("name", Name);
            info.AddValue("melee", Melee);
            info.AddValue("ranged", Ranged);
            info.AddValue("defensive", Defensive);
            info.AddValue("health", Health);
            info.AddValue("movementStrategically", MovementStrategically);
            info.AddValue("movementTactical", MovementTactical);
            info.AddValue("morale", Morale);
            info.AddValue("fortitude", Fortitude);
            info.AddValue("reflex", Reflex);
            info.AddValue("value", Value);
            info.AddValue("upkeep", Upkeep);
        }
    }
}

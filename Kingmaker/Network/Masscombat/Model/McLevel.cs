using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class McLevel : ISerializable
    {
        public int Id;
        public string Name;
        public int Melee;
        public int Ranged;
        public int Defensive;
        public float Health;
        public float MovementStrategically;
        public int MovementTactical;
        public int Morale;
        public int Fortitude;
        public int Reflex;
        public float Value;
        public float Upkeep;
        public int Threat;
        public int XpNeeded;

        public McLevel()
        {
            Name = name;
            Melee = melee;
            Ranged = ranged;
            Defensive = defensive;
            Health = health;
            MovementStrategically = movementStrategically;
            MovementTactical = movementTactical;
            Morale = morale;
            Fortitude = fortitude;
            Reflex = reflex;
            Value = value;
            Upkeep = upkeep;
            Threat = threat;
            XpNeeded = xpNeeded;
        }

        public McLevel(int id, string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, int threat, int xpNeeded)
        {
            Id = id;
            Name = name;
            Melee = melee;
            Ranged = ranged;
            Defensive = defensive;
            Health = health;
            MovementStrategically = movementStrategically;
            MovementTactical = movementTactical;
            Morale = morale;
            Fortitude = fortitude;
            Reflex = reflex;
            Value = value;
            Upkeep = upkeep;
            Threat = threat;
            XpNeeded = xpNeeded;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}

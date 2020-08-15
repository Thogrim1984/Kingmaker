using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    class McEquipment
    {
        public int Id { get; }
        public string Name { get; }
        public int Melee { get; }
        public int Ranged { get; }
        public int Defensive { get; }
        public float Health { get; }
        public float MovementStrategically { get; }
        public int MovementTactical { get; }
        public int Morale { get; }
        public int Fortitude { get; }
        public int Reflex { get; }
        public float Upkeep { get; }
        public Dictionary<NetMcSize, float> Cost { get; }

        public McEquipment(int id, string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float upkeep, Dictionary<NetMcSize,float> cost)
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
            Upkeep = upkeep;
            Cost = cost;
        }

        public McEquipment(string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float upkeep)
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
            Upkeep = upkeep;
        }
    }
}

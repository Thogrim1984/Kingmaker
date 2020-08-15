using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class McPromotion
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
        public float Value { get; }
        public float Upkeep { get; }
        public string Description { get; }
        public string Tooltip { get; }

        public McPromotion(string name, string description, string tooltip)
        {
            Name = name;
            Melee = 0;
            Ranged = 0;
            Defensive = 0;
            Health = 1;
            MovementStrategically = 1;
            MovementTactical = 0;
            Morale = 0;
            Fortitude = 0;
            Reflex = 0;
            Value = 1;
            Upkeep = 1;
            Description = description;
            Tooltip = tooltip;
        }

        public McPromotion(string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, string description, string tooltip)
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
            Description = description;
            Tooltip = tooltip;
        }

        public McPromotion(int id, string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, string description, string tooltip)
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
            Description = description;
            Tooltip = tooltip;
        }
    }
}

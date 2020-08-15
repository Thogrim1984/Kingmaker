using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    [Serializable]
    public class McAbility
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Melee { get; set; }
        public int Ranged { get; set; }
        public int Defensive { get; set; }
        public float Health { get; set; }
        public float MovementStrategically { get; set; }
        public int MovementTactical { get; set; }
        public int Morale { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public float Value { get; set; }
        public float Upkeep { get; set; }
        public String Tooltip { get; set; }
        public String Description { get; set; }

        public McAbility()
        {
        }

        public McAbility(int id, string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, string tooltip, string description)
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
            Tooltip = tooltip;
            Description = description;
        }

        public McAbility(string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, string tooltip, string description)
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
            Tooltip = tooltip;
            Description = description;
        }
    }
}

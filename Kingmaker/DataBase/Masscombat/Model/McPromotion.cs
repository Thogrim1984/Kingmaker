using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McPromotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        public string Description { get; set; }
        public string Tooltip { get; set; }

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

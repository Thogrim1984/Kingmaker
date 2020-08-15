using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Melee { get; set; }
        public int Ranged { get; set; }
        public int Defensive { get; set; }
        public int Health { get; set; }
        public float MovementStrategically { get; set; }
        public int MovementTactical { get; set; }
        public int Morale { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public int Value { get; set; }
        public float Upkeep { get; set; }
        public int ThreatBonus { get; set; }
        public int Squares { get; set; }

        public McSize()
        {
        }

        public McSize(string name, int melee, int ranged, int defensive, int health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, int value, float upkeep, int threatBonus, int squares)
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
            ThreatBonus = threatBonus;
            Squares = squares;
        }

        public McSize(int id, string name, int melee, int ranged, int defensive, int health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, int value, float upkeep, int threatBonus, int squares)
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
            ThreatBonus = threatBonus;
            Squares = squares;
        }
    }
}

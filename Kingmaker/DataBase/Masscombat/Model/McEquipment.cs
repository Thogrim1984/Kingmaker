﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McEquipment
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
        public float Upkeep { get; set; }
        public Dictionary<McSize, float> Cost { get; set; }

        public McEquipment(int id, string name, int melee, int ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float upkeep, Dictionary<McSize,float> cost)
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

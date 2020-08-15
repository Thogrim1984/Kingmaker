using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McRace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Melee { get; set; }
        public Nullable<int> Ranged { get; set; }
        public int Defensive { get; set; }
        public float Health { get; set; }
        public float MovementStrategically { get; set; }
        public int MovementTactical { get; set; }
        public int Morale { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public float Value { get; set; }
        public float Upkeep { get; set; }
        public int Threat { get; set; }
        public bool IsMount { get; set; }
        public int CharismaBase { get; set; }

        public List<McAbility> AbilityList { get; set; }
        public List<McPromotion> PromotionList { get; set; }
        public List<McTactic> TacticList { get; set; }

        public McRace()
        {
        }

        public McRace(string name, int kind, int melee, int? ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, int threat, bool isMount, int charismaBase)
        {
            Name = name;
            Kind = kind;
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
            IsMount = isMount;
            CharismaBase = charismaBase;
        }

        public McRace(int id, string name, int kind, int melee, int? ranged, int defensive, float health, float movementStrategically, int movementTactical, int morale, int fortitude, int reflex, float value, float upkeep, int threat, bool isMount, int charismaBase)
        {
            Id = id;
            Name = name;
            Kind = kind;
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
            IsMount = isMount;
            CharismaBase = charismaBase;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class McRace
    {
        public int Id { get; }
        public string Name { get; }
        public int Kind { get; }
        public int Melee { get; }
        public Nullable<int> Ranged { get; }
        public int Defensive { get; }
        public float Health { get; }
        public float MovementStrategically { get; }
        public int MovementTactical { get; }
        public int Morale { get; }
        public int Fortitude { get; }
        public int Reflex { get; }
        public float Value { get; }
        public float Upkeep { get; }
        public int Threat { get; }
        public bool IsMount { get; }
        public int CharismaBase { get; }

        public List<NetMcAbility> AbilityList { get; }
        public List<McPromotion> PromotionList { get; }
        public List<NetMcTactic> TacticList { get; }

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

            AbilityList = SetAbilityList();
            PromotionList = SetPromotionList();
            TacticList = SetTacticList();
        }

        private List<NetMcAbility> SetAbilityList()
        {
            List<NetMcAbility> abilityList = new List<NetMcAbility>();
            abilityList.AddRange(DBMasscombatCommunication.ReadAbilitiesToRace(Id));

            abilityList.OrderBy(x => x.Name);

            return abilityList;
        }

        private List<McPromotion> SetPromotionList()
        {
            List<McPromotion> promotionList = new List<McPromotion>();
            promotionList.AddRange(DBMasscombatCommunication.ReadPromotionsToRace(Id));

            promotionList.OrderBy(x => x.Name);

            return promotionList;
        }

        private List<NetMcTactic> SetTacticList()
        {
            List<NetMcTactic> tacticList = new List<NetMcTactic>();
            tacticList.AddRange(DBMasscombatCommunication.ReadTacticsToRace(Id));

            tacticList.OrderBy(x => x.Name);

            return tacticList;
        }
    }
}

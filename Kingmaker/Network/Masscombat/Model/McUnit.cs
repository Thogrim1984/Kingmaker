using GSF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Königsmacher.Network.Masscombat.Model
{
    [Serializable]
    class McUnit
    {
        public int Id { get; }
        public string Name { get; }
        public int SizeId { get; }
        public int TypeId { get; }
        public int LevelId { get; }
        public int Xp { get; }
        public int SpecializationId { get; }
        public int RaceId { get; }
        public int FactionId { get; }
        public int CommanderId { get; }
        public string IconString { get; }
        public int MountId { get; }

        public NetMcSize Size { get; }
        public NetMcType Type { get; }
        public McLevel Level { get; }
        public NetMcSpecialization Specialization { get; }
        public McRace Race { get; }
        public NetMcFaction Faction { get; }
        public McCommander Commander { get; }
        public Image Icon { get; }
        public McRace Mount { get; }

        public List<NetMcAbility> AbilityList { get; }
        public List<NetMcCommand> CommandList { get; }
        public List<McEquipment> EquipmentList { get; }
        public List<McPromotion> PromotionList { get; }
        public List<NetMcTactic> TacticList { get; }

        public McUnit(string name, int sizeId, int typeId, int levelId, int xp, int specializationId, int raceId, int factionId, int commanderId, string icon, int mountId)
        {
            Name = name;
            SizeId = sizeId;
            TypeId = typeId;
            LevelId = levelId;
            Xp = xp;
            SpecializationId = specializationId;
            RaceId = raceId;
            FactionId = factionId;
            CommanderId = commanderId;
            IconString = icon;
            MountId = mountId;
        }

        public McUnit(int id, string name, int sizeId, int typeId, int levelId, int xp, int specializationId, int raceId, int factionId, int commanderId, string icon, int mountId)
        {
            Id = id;
            Name = name;
            SizeId = sizeId;
            TypeId = typeId;
            LevelId = levelId;
            Xp = xp;
            SpecializationId = specializationId;
            RaceId = raceId;
            FactionId = factionId;
            CommanderId = commanderId;
            IconString = icon;
            MountId = mountId;

            Size = DBMasscombatCommunication.ReadSizes(new int[1] { SizeId })[0];
            Type = DBMasscombatCommunication.ReadTypes(new int[1] { TypeId })[0];
            Level = DBMasscombatCommunication.ReadLevels(new int[1] { LevelId })[0];
            Specialization = DBMasscombatCommunication.ReadSpecializations(new int[1] { SpecializationId })[0];
            Race = DBMasscombatCommunication.ReadRaces(new int[1] { RaceId })[0];
            Faction = DBMasscombatCommunication.ReadFactions(new int[1] { FactionId })[0];
            Commander = DBMasscombatCommunication.ReadCommanders(new int[1] { CommanderId })[0];
            Icon = Helper.GetImageFromString(IconString);
            if (MountId > 0)
            {
                Mount = DBMasscombatCommunication.ReadRaces(new int[1] { MountId })[0];
            }
            else
            {
                Mount = null;
            }

            AbilityList = SetAbilityList();
            CommandList = SetCommandList();
            EquipmentList = SetEquipmentList();
            PromotionList = SetPromotionList();
            TacticList = SetTacticList();
        }

        #region Lists
        private List<NetMcAbility> SetAbilityList()
        {
            List<NetMcAbility> abilityList = Race.AbilityList;

            foreach (NetMcAbility unitAbility in DBMasscombatCommunication.ReadAbilitiesToUnit(Id))
            {
                bool duplicate = false;
                foreach (NetMcAbility ability in abilityList)
                {
                    if (unitAbility.Id == ability.Id)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    abilityList.Add(unitAbility);
                }
            }

            abilityList.OrderBy(x => x.Name);

            return abilityList;
        }

        private List<NetMcCommand> SetCommandList()
        {
            return Commander.CommandList;
        }

        private List<McEquipment> SetEquipmentList()
        {
            List<McEquipment> equipmentList = DBMasscombatCommunication.ReadEquipmentToUnit(Id).ToList();

            equipmentList.OrderBy(x => x.Name);

            return equipmentList;
        }

        private List<McPromotion> SetPromotionList()
        {
            List<McPromotion> promotionList = Race.PromotionList;

            foreach (McPromotion unitPromotion in DBMasscombatCommunication.ReadPromotionsToUnit(Id))
            {
                bool duplicate = false;
                foreach (McPromotion promotion in promotionList)
                {
                    if (unitPromotion.Id == promotion.Id)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    promotionList.Add(unitPromotion);
                }
            }

            promotionList.OrderBy(x => x.Name);

            return promotionList;
        }

        private List<NetMcTactic> SetTacticList()
        {
            List<NetMcTactic> tacticList = Race.TacticList;

            foreach (NetMcTactic unitTactic in DBMasscombatCommunication.ReadTacticsToUnit(Id))
            {
                bool duplicate = false;
                foreach (NetMcTactic tactic in tacticList)
                {
                    if (unitTactic.Id == tactic.Id)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    tacticList.Add(unitTactic);
                }
            }

            tacticList.OrderBy(x => x.Name);

            return tacticList;
        }
        #endregion

        public int CalculateMelee(int usedStrategicId = 3)
        {
            int melee = Size.Melee + Type.Melee + Level.Melee + Specialization.Melee + Race.Melee + Commander.BonusMelee;

            switch (usedStrategicId)
            {
                case 1:
                    melee += 4;
                    break;
                case 2:
                    melee += 2;
                    break;
                case 4:
                    melee -= 2;
                    break;
                case 5:
                    melee -= 4;
                    break;
                default:
                    break;
            }

            foreach (NetMcAbility ability in AbilityList)
            {
                melee += ability.Melee;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                melee += equipment.Melee;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                melee += promotion.Melee;
            }

            return melee;
        }

        public Nullable<int> CalculateRanged(int usedStrategicId = 3)
        {
            if (Race.Ranged == null)
            {
                return null;
            }

            int ranged = Size.Ranged + Type.Ranged + Level.Ranged + Specialization.Ranged + (int)Race.Ranged + Commander.BonusRanged;

            switch (usedStrategicId)
            {
                case 1:
                    ranged += 4;
                    break;
                case 2:
                    ranged += 2;
                    break;
                case 4:
                    ranged -= 2;
                    break;
                case 5:
                    ranged -= 4;
                    break;
                default:
                    break;
            }

            foreach (NetMcAbility ability in AbilityList)
            {
                ranged += ability.Ranged;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                ranged += equipment.Ranged;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                ranged += promotion.Ranged;
            }

            return ranged;
        }

        public int CalculateDefensive(int usedStrategicId = 3)
        {
            int defensive = Size.Defensive + Type.Defensive + Level.Defensive + Specialization.Defensive + Race.Defensive + Commander.BonusDefensive;

            switch (usedStrategicId)
            {
                case 1:
                    defensive -= 4;
                    break;
                case 2:
                    defensive -= 2;
                    break;
                case 4:
                    defensive += 2;
                    break;
                case 5:
                    defensive += 4;
                    break;
                default:
                    break;
            }

            foreach (NetMcAbility ability in AbilityList)
            {
                defensive += ability.Defensive;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                defensive += equipment.Defensive;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                defensive += promotion.Defensive;
            }

            return defensive;
        }

        public double CalculateMaxHealth()
        {
            float maxHealth = Size.Health + ((Size.Health * Type.Health) - Size.Health) + ((Size.Health * Level.Health) - Size.Health) + ((Size.Health * Specialization.Health) - Size.Health) + ((Size.Health * Race.Health) - Size.Health) + ((Size.Health * Commander.BonusHealth) - Size.Health);

            foreach (NetMcAbility ability in AbilityList)
            {
                maxHealth += ((Size.Health * ability.Health) - Size.Health);
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                maxHealth += ((Size.Health * equipment.Health) - Size.Health);
            }

            foreach (McPromotion promotion in PromotionList)
            {
                maxHealth += ((Size.Health * promotion.Health) - Size.Health);
            }

            return maxHealth;
        }

        public void CalculateCurrentHealth(double changeCurrentHealth)
        {
            throw new NotImplementedException();
        }

        public double CalculateMovementStrategically()
        {
            float movementStrategically = Size.MovementStrategically + Type.MovementStrategically + Level.MovementStrategically + Specialization.MovementStrategically;

            if (Mount != null)
            {
                movementStrategically += Mount.MovementStrategically;
            }
            else
            {
                movementStrategically += Race.MovementStrategically;
            }

            foreach (NetMcAbility ability in AbilityList)
            {
                movementStrategically += ability.MovementStrategically;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                movementStrategically += equipment.MovementStrategically;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                movementStrategically += promotion.MovementStrategically;
            }

            return movementStrategically;
        }

        public int CalculateMovementTactical()
        {
            int movementTactical = Size.MovementTactical + Type.MovementTactical + Level.MovementTactical + Specialization.MovementTactical;

            if (Mount != null)
            {
                movementTactical += Mount.MovementTactical;
            }
            else
            {
                movementTactical += Race.MovementTactical;
            }

            foreach (NetMcAbility ability in AbilityList)
            {
                movementTactical += ability.MovementTactical;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                movementTactical += equipment.MovementTactical;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                movementTactical += promotion.MovementTactical;
            }

            return movementTactical;
        }

        public int CalculateMorale()
        {
            int morale = Size.Morale + Type.Morale + Level.Morale + Specialization.Morale + Race.Morale + Commander.BonusMorale + Commander.Charisma;

            foreach (NetMcAbility ability in AbilityList)
            {
                morale += ability.Morale;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                morale += equipment.Morale;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                morale += promotion.Morale;
            }

            return morale;
        }

        public int CalculateFortitude()
        {
            int fortitude = Size.Fortitude + Type.Fortitude + Level.Fortitude + Specialization.Fortitude + Race.Fortitude + Commander.BonusFortitude;

            foreach (NetMcAbility ability in AbilityList)
            {
                fortitude += ability.Fortitude;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                fortitude += equipment.Fortitude;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                fortitude += promotion.Fortitude;
            }

            return fortitude;
        }

        public int CalculateReflex()
        {
            int reflex = Size.Reflex + Type.Reflex + Level.Reflex + Specialization.Reflex + Race.Reflex + Commander.BonusReflex;

            foreach (NetMcAbility ability in AbilityList)
            {
                reflex += ability.Reflex;
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                reflex += equipment.Reflex;
            }

            foreach (McPromotion promotion in PromotionList)
            {
                reflex += promotion.Reflex;
            }

            return reflex;
        }

        public double CalculateValue()
        {
            float value = Size.Value + ((Size.Value * Type.Value) - Size.Value) + ((Size.Value * Level.Value) - Size.Value) + ((Size.Value * Specialization.Value) - Size.Value) + ((Size.Value * Race.Value) - Size.Value);

            foreach (NetMcAbility ability in AbilityList)
            {
                value += ((Size.Value * ability.Value) - Size.Value);
            }

            foreach (McPromotion promotion in PromotionList)
            {
                value += ((Size.Value * promotion.Value) - Size.Value);
            }

            return value;
        }

        public double CalculateUpkeep()
        {
            float upkeep = Size.Upkeep + ((Size.Upkeep * Type.Upkeep) - Size.Upkeep) + ((Size.Upkeep * Level.Upkeep) - Size.Upkeep) + ((Size.Upkeep * Specialization.Upkeep) - Size.Upkeep) + ((Size.Upkeep * Race.Upkeep) - Size.Upkeep) + ((Size.Upkeep * Commander.BonusUpkeep) - Size.Upkeep);

            foreach (NetMcAbility ability in AbilityList)
            {
                upkeep += ((Size.Upkeep * ability.Upkeep) - Size.Upkeep);
            }

            foreach (McEquipment equipment in EquipmentList)
            {
                upkeep += ((Size.Upkeep * equipment.Upkeep) - Size.Upkeep);
            }

            foreach (McPromotion promotion in PromotionList)
            {
                upkeep += ((Size.Upkeep * promotion.Upkeep) - Size.Upkeep);
            }

            return upkeep;
        }
    }
}

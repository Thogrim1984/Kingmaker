using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McCommander
    {
        public int leadershipRoleId { get; set; }
        public int factionId { get; set; }
        public int mentalAbility { get; set; }
        public Boolean isCouncilPosition { get; set; }
        public int hitDice { get; set; }
        public Boolean isFeatLeadership { get; set; }
        public int bonusLeadership { get; set; }
        public int professionSoldier { get; set; }

        public int Id { get; set; }
        public String Name { get; set; }
        public int Charisma { get; set; }
        public int BonusMelee { get; set; }
        public int BonusRanged { get; set; }
        public int BonusDefensive { get; set; }
        public int BonusHealth { get; set; }
        public int BonusMorale { get; set; }
        public int BonusFortitude { get; set; }
        public int BonusReflex { get; set; }
        public int BonusUpkeep { get; set; }
        public String Token { get; set; }
        public List<McCommand> CommandList { get; set; }
        public McFaction Faction { get; set; }
        public int LeadershipScore { get; set; }
        public McLeadershipRole LeadershipRole { get; set; }

        public McCommander()
        {
        }

        public McCommander(int id, string name, int charisma, int mentalAbility, int professionSoldier, int bonusMelee, int bonusRanged, int bonusDefensive, int bonusHealth, int bonusMorale, int bonusFortitude, int bonusReflex, int bonusUpkeep, bool isCouncilPosition, int leadershipRoleId, int hitDice, bool isFeatLeadership, int bonusLeadership, int factionId, String token)
        {
            Id = id;
            Name = name;
            Charisma = charisma;
            this.mentalAbility = mentalAbility;
            this.professionSoldier = professionSoldier;
            BonusMelee = bonusMelee;
            BonusRanged = bonusRanged;
            BonusDefensive = bonusDefensive;
            BonusHealth = bonusHealth;
            BonusMorale = bonusMorale;
            BonusFortitude = bonusFortitude;
            BonusReflex = bonusReflex;
            BonusUpkeep = bonusUpkeep;
            this.isCouncilPosition = isCouncilPosition;
            this.leadershipRoleId = leadershipRoleId;
            this.hitDice = hitDice;
            this.isFeatLeadership = isFeatLeadership;
            this.bonusLeadership = bonusLeadership;
            this.factionId = factionId;
            Token = token;
        }

        public McCommander(string name, int charisma, int mentalAbility, int professionSoldier, int bonusMelee, int bonusRanged, int bonusDefensive, int bonusHealth, int bonusMorale, int bonusFortitude, int bonusReflex, int bonusUpkeep, bool isCouncilPosition, int leadershipRoleId, int hitDice, bool isFeatLeadership, int bonusLeadership, int factionId, String token)
        {
            Name = name;
            Charisma = charisma;
            this.mentalAbility = mentalAbility;
            this.professionSoldier = professionSoldier;
            BonusMelee = bonusMelee;
            BonusRanged = bonusRanged;
            BonusDefensive = bonusDefensive;
            BonusHealth = bonusHealth;
            BonusMorale = bonusMorale;
            BonusFortitude = bonusFortitude;
            BonusReflex = bonusReflex;
            BonusUpkeep = bonusUpkeep;
            this.isCouncilPosition = isCouncilPosition;
            this.leadershipRoleId = leadershipRoleId;
            this.hitDice = hitDice;
            this.isFeatLeadership = isFeatLeadership;
            this.bonusLeadership = bonusLeadership;
            this.factionId = factionId;
            Token = token;
        }
    }
}

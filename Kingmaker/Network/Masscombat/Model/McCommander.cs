using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Königsmacher.Network.Masscombat.Model
{
    class McCommander
    {
        //public int leadershipRoleId { get; }
        //public int factionId { get; }
        //public int mentalAbility { get; }
        //public Boolean isCouncilPosition { get; }
        //public int hitDice { get; }
        //public Boolean isFeatLeadership { get; }
        //public int bonusLeadership { get; }
        //public int professionSoldier { get; }

        //public int Id { get; }
        //public String Name { get; }
        //public int Charisma { get; }
        //public int BonusMelee { get; }
        //public int BonusRanged { get; }
        //public int BonusDefensive { get; }
        //public int BonusHealth { get; }
        //public int BonusMorale { get; }
        //public int BonusFortitude { get; }
        //public int BonusReflex { get; }
        //public int BonusUpkeep { get; }
        //public Image Token { get; }
        //public List<McCommand> CommandList { get; }
        //public McFaction Faction { get; }
        //public int LeadershipScore { get; }
        //public McLeadershipRole LeadershipRole { get; }

        //public McCommander(int id, string name, int charisma, int mentalAbility, int professionSoldier, int bonusMelee, int bonusRanged, int bonusDefensive, int bonusHealth, int bonusMorale, int bonusFortitude, int bonusReflex, int bonusUpkeep, bool isCouncilPosition, int leadershipRoleId, int hitDice, bool isFeatLeadership, int bonusLeadership, int factionId, Image token)
        //{
        //    Id = id;
        //    Name = name;
        //    Charisma = charisma;
        //    this.mentalAbility = mentalAbility;
        //    this.professionSoldier = professionSoldier;
        //    BonusMelee = bonusMelee;
        //    BonusRanged = bonusRanged;
        //    BonusDefensive = bonusDefensive;
        //    BonusHealth = bonusHealth;
        //    BonusMorale = bonusMorale;
        //    BonusFortitude = bonusFortitude;
        //    BonusReflex = bonusReflex;
        //    BonusUpkeep = bonusUpkeep;
        //    this.isCouncilPosition = isCouncilPosition;
        //    this.leadershipRoleId = leadershipRoleId;
        //    this.hitDice = hitDice;
        //    this.isFeatLeadership = isFeatLeadership;
        //    this.bonusLeadership = bonusLeadership;
        //    this.factionId = factionId;
        //    Token = token;

        //    LeadershipRole = SetLeadershipRole();
        //    CommandList = SetCommandList();
        //    Faction = SetFaction();
        //    LeadershipScore = SetLeadershipScore();
        //}

        //public McCommander(string name, int charisma, int mentalAbility, int professionSoldier, int bonusMelee, int bonusRanged, int bonusDefensive, int bonusHealth, int bonusMorale, int bonusFortitude, int bonusReflex, int bonusUpkeep, bool isCouncilPosition, int leadershipRoleId, int hitDice, bool isFeatLeadership, int bonusLeadership, int factionId, Image token)
        //{
        //    Name = name;
        //    Charisma = charisma;
        //    this.mentalAbility = mentalAbility;
        //    this.professionSoldier = professionSoldier;
        //    BonusMelee = bonusMelee;
        //    BonusRanged = bonusRanged;
        //    BonusDefensive = bonusDefensive;
        //    BonusHealth = bonusHealth;
        //    BonusMorale = bonusMorale;
        //    BonusFortitude = bonusFortitude;
        //    BonusReflex = bonusReflex;
        //    BonusUpkeep = bonusUpkeep;
        //    this.isCouncilPosition = isCouncilPosition;
        //    this.leadershipRoleId = leadershipRoleId;
        //    this.hitDice = hitDice;
        //    this.isFeatLeadership = isFeatLeadership;
        //    this.bonusLeadership = bonusLeadership;
        //    this.factionId = factionId;
        //    Token = token;
        //}

        //private McLeadershipRole SetLeadershipRole()
        //{
        //    if (leadershipRoleId > 0)
        //    {
        //        return DBMasscombatCommunication.ReadLeadershipRoles(new int[1] { leadershipRoleId })[0];
        //    }

        //    return null;
        //}

        //private int SetLeadershipScore()
        //{
        //    int leadershipScore = Math.Max(hitDice, professionSoldier);

        //    leadershipScore += mentalAbility;

        //    if (isCouncilPosition || LeadershipRole.MainDepartment)
        //    {
        //        leadershipScore += 5;
        //    }
        //    else if (LeadershipRole != null)
        //    {
        //        leadershipScore += 3;
        //    }

        //    if (isFeatLeadership)
        //    {
        //        leadershipScore += 3;
        //    }

        //    if (LeadershipRole.Id == 1 || LeadershipRole.Id == 2 || LeadershipRole.Id == 3 || LeadershipRole.Id == 4 || LeadershipRole.Id == 5 || LeadershipRole.Id == 6 || LeadershipRole.Id == 14 || LeadershipRole.Id == 21)
        //    {
        //        leadershipScore += 1;
        //    }

        //    leadershipScore += bonusLeadership;

        //    return leadershipScore;
        //}

        //private McFaction SetFaction()
        //{
        //    return DBMasscombatCommunication.ReadFactions(new int[1] { factionId })[0];
        //}

        //private List<McCommand> SetCommandList()
        //{
        //    List<McCommand> commandList = new List<McCommand>();
        //    commandList.AddRange(DBMasscombatCommunication.ReadCommandsToCommander(Id));

        //    if (LeadershipRole != null)
        //    {
        //        bool duplicate = false;
        //        McCommand lrCommand = DBMasscombatCommunication.ReadCommands(new int[1] { LeadershipRole.CommandId })[0];

        //        foreach (McCommand command in commandList)
        //        {
        //            if (command.Id == lrCommand.Id)
        //            {
        //                duplicate = true;
        //                break;
        //            }
        //        }

        //        if (!duplicate)
        //        {
        //            commandList.Add(lrCommand);
        //        }
        //    }

        //    commandList.OrderBy(x => x.Name);

        //    return commandList;
        //}
    }
}

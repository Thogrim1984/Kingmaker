using Kingmaker.DataBase.Masscombat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat
{
    public class McModelFactory
    {
        internal List<McAbility> getAbilities(int[] ids)
        {
            List<McAbility> abilityList = McDbCommunication.ReadAbilities(ids).ToList();
            abilityList.OrderBy(x => x.Name);

            return abilityList;
        }

        internal List<McCommand> GetCommands(int[] ids)
        {
            List<McCommand> commandList = McDbCommunication.ReadCommands(ids).ToList();
            commandList.OrderBy(x => x.Name);

            return commandList;
        }

        internal List<McCommander> GetCommanders(int[] ids)
        {
            List<McCommander> commanderList = new List<McCommander>();

            #region Commanders
            foreach (var item in McDbCommunication.ReadCommanders(ids))
            {
                commanderList.Add(item);
            }
            #endregion

            foreach (var item in commanderList)
            {
                #region Leadership Role
                if (item.leadershipRoleId < 1)
                {
                    item.LeadershipRole = null;
                }
                else
                {
                    item.LeadershipRole = GetLeadershipRoles(new int[1] { item.leadershipRoleId })[0];
                }
                #endregion

                #region Commands
                List<McCommand> commandList = new List<McCommand>();
                commandList.AddRange(GetCommands(McDbCommunication.ReadCommandIdsToCommander(item.Id)));

                if (item.LeadershipRole != null)
                {
                    bool duplicate = false;
                    McCommand lrCommand = GetCommands(new int[1] { item.LeadershipRole.CommandId })[0];

                    foreach (McCommand command in commandList)
                    {
                        if (command.Id == lrCommand.Id)
                        {
                            duplicate = true;
                            break;
                        }
                    }

                    if (!duplicate)
                    {
                        commandList.Add(lrCommand);
                    }
                }

                commandList.OrderBy(x => x.Name);

                item.CommandList = commandList;
                #endregion

                #region Faction
                if (item.factionId < 1)
                {
                    item.Faction = null;
                }
                else
                {
                    item.Faction = GetFactions(new int[1] { item.factionId })[0];
                }
                #endregion

                #region Leadership Score
                int leadershipScore = Math.Max(item.hitDice, item.professionSoldier);

                leadershipScore += item.mentalAbility;

                if (item.isCouncilPosition || item.LeadershipRole.MainDepartment)
                {
                    leadershipScore += 5;
                }
                else if (item.LeadershipRole != null)
                {
                    leadershipScore += 3;
                }

                if (item.isFeatLeadership)
                {
                    leadershipScore += 3;
                }

                if (item.LeadershipRole.Id == 1 || item.LeadershipRole.Id == 2 || item.LeadershipRole.Id == 3 || item.LeadershipRole.Id == 4 || item.LeadershipRole.Id == 5 || item.LeadershipRole.Id == 6 || item.LeadershipRole.Id == 14 || item.LeadershipRole.Id == 21)
                {
                    leadershipScore += 1;
                }

                leadershipScore += item.bonusLeadership;

                item.LeadershipScore = leadershipScore;
                #endregion
            }

            commanderList.OrderBy(x => x.Name);

            return commanderList;
        }

        internal List<McEquipment> GetEquipment(int[] ids)
        {

            List<McEquipment> equipmentList = McDbCommunication.ReadEquipment(ids).ToList();

            foreach (var item in equipmentList)
            {
                Dictionary<McSize, float> cost = new Dictionary<McSize, float>();

                foreach (var result in McDbCommunication.ReadEquipmentCost(item.Id))
                {
                    cost.Add(GetSizes(new int[] { result.Key })[0], result.Value);
                }

                item.Cost = cost;
            }

            equipmentList.OrderBy(x => x.Name);

            return equipmentList;
        }

        internal List<McFaction> GetFactions(int[] ids)
        {
            {
                List<McFaction> factionList = McDbCommunication.ReadFactions(ids).ToList();
                factionList.OrderBy(x => x.Name);

                return factionList;
            }
        }

        internal List<McLeadershipRole> GetLeadershipRoles(int[] ids)
        {
            {
                List<McLeadershipRole> leadershipRoleList = McDbCommunication.ReadLeadershipRoles(ids).ToList();
                leadershipRoleList.OrderBy(x => x.Name);

                return leadershipRoleList;
            }
        }

        internal List<McLevel> GetLevels(int[] ids)
        {
            {
                List<McLevel> levelList = McDbCommunication.ReadLevels(ids).ToList();
                levelList.OrderBy(x => x.Name);

                return levelList;
            }
        }

        internal List<McPromotion> GetPromotions(int[] ids)
        {
            {
                List<McPromotion> promotionList = McDbCommunication.ReadPromotions(ids).ToList();
                promotionList.OrderBy(x => x.Name);

                return promotionList;
            }
        }

        internal List<McRace> GetRaces(int[] ids)
        {
            List<McRace> raceList = new List<McRace>();

            #region Races
            foreach (var item in McDbCommunication.ReadRaces(ids))
            {
                raceList.Add(item);
            }
            #endregion

            foreach (var item in raceList)
            {
                List<McAbility> abilityList = new List<McAbility>();
                abilityList.AddRange(getAbilities(McDbCommunication.ReadAbilityIdsToRace(item.Id)));

                item.AbilityList = abilityList;

                List<McPromotion> promotionList = new List<McPromotion>();
                promotionList.AddRange(GetPromotions(McDbCommunication.ReadPromotionIdsToRace(item.Id)));

                item.PromotionList = promotionList;

                List<McTactic> tacticList = new List<McTactic>();
                tacticList.AddRange(GetTactics(McDbCommunication.ReadTacticIdsToRace(item.Id)));

                item.TacticList = tacticList;
            }

            raceList.OrderBy(x => x.Name);

            return raceList;
        }

        internal List<McSize> GetSizes(int[] ids)
        {
            List<McSize> sizeList = McDbCommunication.ReadSizes(ids).ToList();
            sizeList.OrderBy(x => x.Name);

            return sizeList;
        }

        internal List<McSpecialization> GetSpecializations(int[] ids)
        {
            List<McSpecialization> specializationList = McDbCommunication.ReadSpecializations(ids).ToList();
            specializationList.OrderBy(x => x.Name);

            return specializationList;
        }

        internal List<McTactic> GetTactics(int[] ids)
        {
            List<McTactic> tacticList = McDbCommunication.ReadTactics(ids).ToList();
            tacticList.OrderBy(x => x.Name);

            return tacticList;
        }

        internal List<McType> GetType(int[] ids)
        {
            List<McType> typeList = McDbCommunication.ReadTypes(ids).ToList();
            typeList.OrderBy(x => x.Name);

            return typeList;
        }

        internal List<McUnit> GetUnits(int[] ids)
        {
            List<McUnit> unitList = McDbCommunication.ReadUnits(ids).ToList();

            foreach (var item in unitList)
            {
                item.Size = McDbCommunication.ReadSizes(new int[1] { item.SizeId })[0];
                item.Type = McDbCommunication.ReadTypes(new int[1] { item.TypeId })[0];
                item.Level = McDbCommunication.ReadLevels(new int[1] { item.LevelId })[0];
                item.Specialization = McDbCommunication.ReadSpecializations(new int[1] { item.SpecializationId })[0];
                item.Race = McDbCommunication.ReadRaces(new int[1] { item.RaceId })[0];                
                item.Faction = item.FactionId < 1 ? null : McDbCommunication.ReadFactions(new int[1] { item.FactionId })[0];
                item.Commander = item.CommanderId < 1 ? null : McDbCommunication.ReadCommanders(new int[1] { item.CommanderId })[0];

            }



            return unitList;
        }
    }
}

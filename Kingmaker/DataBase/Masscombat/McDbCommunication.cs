using Kingmaker.DataBase.Masscombat.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Kingmaker.DataBase.Masscombat
{
    static class McDbCommunication
    {
        #region Abilities 
        #region Read Abilities
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McAbility; null if no valid Abilities are found
        /// </summary>
        public static McAbility[] ReadAbilities(int[] ids)
        {
            McAbility[] abilities;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        abilities = new McAbility[1];
                        using (var db = new MasscombatContext())
                        {
                            var ability = db.masscombat_ability.SingleOrDefault(x => x.i_id == ids[0]);
                            abilities[0] = new McAbility(ability.i_id, ability.vc_ability, ability.i_melee, ability.i_ranged, ability.i_defensive, ability.fl_health, ability.fl_movement_strategically, ability.i_movement_tactical, ability.i_morale, ability.i_fortitude, ability.i_reflex, ability.fl_value, ability.fl_upkeep, ability.vc_tooltip, ability.vc_description);
                            
                        }
                    }
                    else // all IDs
                    {
                        List<McAbility> abilityList = new List<McAbility>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_ability
                                        orderby b.i_id
                                        select b;

                            foreach (var ability in query)
                            {
                                abilityList.Add(new McAbility(ability.i_id, ability.vc_ability, ability.i_melee, ability.i_ranged, ability.i_defensive, ability.fl_health, ability.fl_movement_strategically, ability.i_movement_tactical, ability.i_morale, ability.i_fortitude, ability.i_reflex, ability.fl_value, ability.fl_upkeep, ability.vc_tooltip, ability.vc_description));
                            }
                        }
                        abilities = abilityList.ToArray();
                    }
                }
                else // Several IDs
                {
                    abilities = new McAbility[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var ability = db.masscombat_ability.SingleOrDefault(x => x.i_id == id);
                            abilities[i] = new McAbility(ability.i_id, ability.vc_ability, ability.i_melee, ability.i_ranged, ability.i_defensive, ability.fl_health, ability.fl_movement_strategically, ability.i_movement_tactical, ability.i_morale, ability.i_fortitude, ability.i_reflex, ability.fl_value, ability.fl_upkeep, ability.vc_tooltip, ability.vc_description);
                        }
                    }
                }
                return abilities;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Abilities
        /// <summary>
        /// Params: 1 or more McAbility
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteAbilities(McAbility[] abilities)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < abilities.Length; i++)
                    {
                        var ability = new masscombat_ability { vc_ability = abilities[i].Name, i_melee = abilities[i].Melee, i_ranged = abilities[i].Ranged, i_defensive = abilities[i].Defensive, fl_health = abilities[i].Health, fl_movement_strategically = abilities[i].MovementStrategically, i_movement_tactical = abilities[i].MovementTactical, i_morale = abilities[i].Morale, i_fortitude = abilities[i].Fortitude, i_reflex = abilities[i].Reflex, fl_value = abilities[i].Value, fl_upkeep = abilities[i].Upkeep, vc_tooltip = abilities[i].Tooltip, vc_description = abilities[i].Description };
                        db.masscombat_ability.Add(ability);
                        db.SaveChanges();

                        WrittenIds.Add(ability.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (abilities.Length == 0)
                {
                    Debug.Write("No Ability");
                }
                else
                {
                    Debug.Write("Abilities: ");
                    for (int i = 0; i < abilities.Length; i++)
                    {
                        Debug.Write(abilities[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Abilities
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteAbilities(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var ability = db.masscombat_ability.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_ability.Remove(ability);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Ability
        /// <summary>
        /// Returns: ID of the Ability or -1 when some Error occurs
        /// </summary>
        public static int UpdateAbility(McAbility mcAbility)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var ability = db.masscombat_ability.SingleOrDefault(x => x.i_id == mcAbility.Id);

                    ability.vc_ability = mcAbility.Name;
                    ability.i_melee = mcAbility.Melee;
                    ability.i_ranged = mcAbility.Ranged;
                    ability.i_defensive = mcAbility.Defensive;
                    ability.fl_health = mcAbility.Health;
                    ability.fl_movement_strategically = mcAbility.MovementStrategically;
                    ability.i_movement_tactical = mcAbility.MovementTactical;
                    ability.i_morale = mcAbility.Morale;
                    ability.i_fortitude = mcAbility.Fortitude;
                    ability.i_reflex = mcAbility.Reflex;
                    ability.fl_value = mcAbility.Value;
                    ability.fl_upkeep = mcAbility.Upkeep;
                    ability.vc_tooltip = mcAbility.Tooltip;
                    ability.vc_description = mcAbility.Description;
                    db.SaveChanges();

                    return ability.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Commands 
        #region Read Commands 
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McCommand; null if no valid Commands are found
        /// </summary>
        public static McCommand[] ReadCommands(int[] ids)
        {
            McCommand[] commands;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        commands = new McCommand[1];
                        using (var db = new MasscombatContext())
                        {
                            var command = db.masscombat_command.SingleOrDefault(x => x.i_id == ids[0]);
                            commands[0] = new McCommand(command.i_id, command.vc_command, command.i_command_variable, command.vc_tooltip, command.vc_description);
                        }
                    }
                    else // all IDs
                    {
                        List<McCommand> commandList = new List<McCommand>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_command
                                        orderby b.i_id
                                        select b;

                            foreach (var command in query)
                            {
                                commandList.Add(new McCommand(command.i_id, command.vc_command, command.i_command_variable, command.vc_tooltip, command.vc_description));
                            }
                        }
                        commands = commandList.ToArray();
                    }
                }
                else // Several IDs
                {
                    commands = new McCommand[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var command = db.masscombat_command.SingleOrDefault(x => x.i_id == id);
                            commands[i] = new McCommand(command.i_id, command.vc_command, command.i_command_variable, command.vc_tooltip, command.vc_description);
                        }
                    }
                }
                return commands;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Commands
        /// <summary>
        /// Params: 1 or more McCommand
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteCommands(McCommand[] commands)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < commands.Length; i++)
                    {
                        var command = new masscombat_command { vc_command = commands[i].Name, i_command_variable = commands[i].CommandVariable, vc_tooltip = commands[i].Tooltip, vc_description = commands[i].Description };
                        db.masscombat_command.Add(command);
                        db.SaveChanges();

                        WrittenIds.Add(command.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (commands.Length == 0)
                {
                    Debug.Write("No Command");
                }
                else
                {
                    Debug.Write("Commands: ");
                    for (int i = 0; i < commands.Length; i++)
                    {
                        Debug.Write(commands[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Commands
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteCommands(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var command = db.masscombat_command.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_command.Remove(command);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Command
        /// <summary>
        /// Returns: ID of the Command or -1 when some Error occurs
        /// </summary>
        public static int UpdateCommand(McCommand mcCommand)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var command = db.masscombat_command.SingleOrDefault(x => x.i_id == mcCommand.Id);

                    command.vc_command = mcCommand.Name;
                    command.i_command_variable = mcCommand.CommandVariable;
                    command.vc_tooltip = mcCommand.Tooltip;
                    command.vc_description = mcCommand.Description;
                    db.SaveChanges();

                    return command.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Commanders 
        #region Read Commanders 
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McCommander; null if no valid Commanders are found
        /// </summary>
        public static McCommander[] ReadCommanders(int[] ids)
        {
            if(ids[0] == 0 || ids[0] < -1)
            {
                return null;
            }

            McCommander[] commanders;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        commanders = new McCommander[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var commander = db.masscombat_commander.SingleOrDefault(x => x.i_id == id);
                            commanders[0] = new McCommander(commander.i_id, commander.vc_commander, commander.i_charisma, commander.i_mental_ability, commander.i_profession_soldier, commander.i_bonus_melee, commander.i_bonus_ranged, commander.i_bonus_defensive, commander.i_bonus_health, commander.i_bonus_morale, commander.i_bonus_fortitude, commander.i_bonus_reflex, commander.i_bonus_upkeep, commander.b_council_position, commander.i_leadership_role_id, commander.i_hit_dice, commander.b_feat_leadership, commander.i_bonus_leadership, (int)commander.i_faction_id, Helper.GetImageFromString(commander.t_token));

                        }
                    }
                    else // all IDs
                    {
                        List<McCommander> commanderList = new List<McCommander>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_commander
                                        orderby b.i_id
                                        select b;

                            foreach (var commander in query)
                            {
                                commanderList.Add(new McCommander(commander.i_id, commander.vc_commander, commander.i_charisma, commander.i_mental_ability, commander.i_profession_soldier, commander.i_bonus_melee, commander.i_bonus_ranged, commander.i_bonus_defensive, commander.i_bonus_health, commander.i_bonus_morale, commander.i_bonus_fortitude, commander.i_bonus_reflex, commander.i_bonus_upkeep, commander.b_council_position, commander.i_leadership_role_id, commander.i_hit_dice, commander.b_feat_leadership, commander.i_bonus_leadership, (int)commander.i_faction_id, Helper.GetImageFromString(commander.t_token)));
                            }
                        }
                        commanders = commanderList.ToArray();
                    }
                }
                else // Several IDs
                {
                    commanders = new McCommander[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var commander = db.masscombat_commander.SingleOrDefault(x => x.i_id == id);
                            commanders[i] = new McCommander(commander.i_id, commander.vc_commander, commander.i_charisma, commander.i_mental_ability, commander.i_profession_soldier, commander.i_bonus_melee, commander.i_bonus_ranged, commander.i_bonus_defensive, commander.i_bonus_health, commander.i_bonus_morale, commander.i_bonus_fortitude, commander.i_bonus_reflex, commander.i_bonus_upkeep, commander.b_council_position, commander.i_leadership_role_id, commander.i_hit_dice, commander.b_feat_leadership, commander.i_bonus_leadership, (int)commander.i_faction_id, Helper.GetImageFromString(commander.t_token));
                        }
                    }
                }
                return commanders;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Commanders
        /// <summary>
        /// Params: 1 or more McCommander
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteCommanders(McCommander[] commanders)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < commanders.Length; i++)
                    {
                        var commander = new masscombat_commander { vc_commander = commanders[i].Name, i_charisma = commanders[i].Charisma, i_mental_ability = commanders[i].mentalAbility, i_profession_soldier = commanders[i].professionSoldier, i_bonus_melee = commanders[i].BonusMelee, i_bonus_ranged = commanders[i].BonusRanged, i_bonus_defensive = commanders[i].BonusDefensive, i_bonus_health = commanders[i].BonusHealth, i_bonus_morale = commanders[i].BonusMorale, i_bonus_fortitude = commanders[i].BonusFortitude, i_bonus_reflex = commanders[i].BonusReflex, i_bonus_upkeep = commanders[i].BonusUpkeep, b_council_position = commanders[i].isCouncilPosition, i_leadership_role_id = commanders[i].leadershipRoleId, i_hit_dice = commanders[i].hitDice, b_feat_leadership = commanders[i].isFeatLeadership, i_bonus_leadership = commanders[i].bonusLeadership, i_faction_id = commanders[i].factionId, t_token = Helper.GetStringFromImage(commanders[0].Token) };
                        db.masscombat_commander.Add(commander);
                        db.SaveChanges();

                        WrittenIds.Add(commander.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (commanders.Length == 0)
                {
                    Debug.Write("No Commander");
                }
                else
                {
                    Debug.Write("Commanders: ");
                    for (int i = 0; i < commanders.Length; i++)
                    {
                        Debug.Write(commanders[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Commanders
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteCommanders(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var commander = db.masscombat_commander.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_commander.Remove(commander);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Commander
        /// <summary>
        /// Returns: ID of the Command or -1 when some Error occurs
        /// </summary>
        public static int UpdateCommander(McCommander mcCommander)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var commander = db.masscombat_commander.SingleOrDefault(x => x.i_id == mcCommander.Id);

                    commander.vc_commander = mcCommander.Name;
                    commander.i_charisma = mcCommander.Charisma;
                    commander.i_mental_ability = mcCommander.mentalAbility;
                    commander.i_profession_soldier = mcCommander.professionSoldier;
                    commander.i_bonus_melee = mcCommander.BonusMelee;
                    commander.i_bonus_ranged = mcCommander.BonusRanged;
                    commander.i_bonus_defensive = mcCommander.BonusDefensive;
                    commander.i_bonus_health = mcCommander.BonusHealth;
                    commander.i_bonus_morale = mcCommander.BonusMorale;
                    commander.i_bonus_fortitude = mcCommander.BonusFortitude;
                    commander.i_bonus_reflex = mcCommander.BonusReflex;
                    commander.i_bonus_upkeep = mcCommander.BonusUpkeep;
                    commander.b_council_position = mcCommander.isCouncilPosition;
                    commander.i_leadership_role_id = mcCommander.leadershipRoleId;
                    commander.i_hit_dice = mcCommander.hitDice;
                    commander.b_feat_leadership = mcCommander.isFeatLeadership;
                    commander.i_bonus_leadership = mcCommander.bonusLeadership;
                    commander.i_faction_id = mcCommander.factionId;
                    commander.t_token = Helper.GetStringFromImage(mcCommander.Token);
                    db.SaveChanges();

                    return commander.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Equipment 
        #region Read Equipment
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McEquipment; null if no valid Abilities are found
        /// </summary>
        public static McEquipment[] ReadEquipment(int[] ids)
        {
            McEquipment[] equipmentArray;
            try
            {
                McSize[] sizes = ReadSizes(new int[] { -1 });

                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        Dictionary<McSize, float> cost = new Dictionary<McSize, float>();
                        using (var db = new MasscombatContext())
                        {

                            var equipmentCost = db.masscombat_equipment_costs.Where(x => x.i_equipment_id == ids[0]);

                            foreach (var item in equipmentCost)
                            {
                                cost.Add(sizes[item.i_unit_size_id - 1], item.fl_costs);
                            }
                        }

                        equipmentArray = new McEquipment[1];
                        using (var db = new MasscombatContext())
                        {
                            var equipment = db.masscombat_equipment.SingleOrDefault(x => x.i_id == ids[0]);
                            equipmentArray[0] = new McEquipment(equipment.i_id, equipment.vc_equipment, equipment.i_melee, equipment.i_ranged, equipment.i_defensive, equipment.fl_health, equipment.fl_movement_strategically, equipment.i_movement_tactical, equipment.i_morale, equipment.i_fortitude, equipment.i_reflex, equipment.fl_upkeep, cost);
                        }
                    }
                    else // all IDs
                    {
                        List<McEquipment> equipmentList = new List<McEquipment>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_equipment
                                        orderby b.i_id
                                        select b;

                            foreach (var equipment in query)
                            {
                                Dictionary<McSize, float> cost = new Dictionary<McSize, float>();
                                using (var dbCosts = new MasscombatContext())
                                {

                                    var equipmentCost = dbCosts.masscombat_equipment_costs.Where(x => x.i_equipment_id == equipment.i_id);

                                    foreach (var item in equipmentCost)
                                    {
                                        cost.Add(sizes[item.i_unit_size_id - 1], item.fl_costs);
                                    }
                                }

                                equipmentList.Add(new McEquipment(equipment.i_id, equipment.vc_equipment, equipment.i_melee, equipment.i_ranged, equipment.i_defensive, equipment.fl_health, equipment.fl_movement_strategically, equipment.i_movement_tactical, equipment.i_morale, equipment.i_fortitude, equipment.i_reflex, equipment.fl_upkeep, cost));
                            }
                        }
                        equipmentArray = equipmentList.ToArray();
                    }
                }
                else // Several IDs
                {
                    equipmentArray = new McEquipment[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var equipment = db.masscombat_equipment.SingleOrDefault(x => x.i_id == id);

                            Dictionary<McSize, float> cost = new Dictionary<McSize, float>();
                            using (var dbCosts = new MasscombatContext())
                            {

                                var equipmentCost = dbCosts.masscombat_equipment_costs.Where(x => x.i_equipment_id == equipment.i_id);

                                foreach (var item in equipmentCost)
                                {
                                    cost.Add(sizes[item.i_unit_size_id - 1], item.fl_costs);
                                }
                            }

                            equipmentArray[i] = new McEquipment(equipment.i_id, equipment.vc_equipment, equipment.i_melee, equipment.i_ranged, equipment.i_defensive, equipment.fl_health, equipment.fl_movement_strategically, equipment.i_movement_tactical, equipment.i_morale, equipment.i_fortitude, equipment.i_reflex, equipment.fl_upkeep, cost);
                        }
                    }
                }
                return equipmentArray;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Equipment
        /// <summary>
        /// Params: 1 or more McEquipment
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteEquipment(McEquipment[] equipmentArray)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < equipmentArray.Length; i++)
                    {
                        var equipment = new masscombat_equipment { vc_equipment = equipmentArray[i].Name, i_melee = equipmentArray[i].Melee, i_ranged = equipmentArray[i].Ranged, i_defensive = equipmentArray[i].Defensive, fl_health = equipmentArray[i].Health, fl_movement_strategically = equipmentArray[i].MovementStrategically, i_movement_tactical = equipmentArray[i].MovementTactical, i_morale = equipmentArray[i].Morale, i_fortitude = equipmentArray[i].Fortitude, i_reflex = equipmentArray[i].Reflex, fl_upkeep = equipmentArray[i].Upkeep };
                        db.masscombat_equipment.Add(equipment);
                        db.SaveChanges();

                        WrittenIds.Add(equipment.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (equipmentArray.Length == 0)
                {
                    Debug.Write("No Equipment");
                }
                else
                {
                    Debug.Write("Equipment: ");
                    for (int i = 0; i < equipmentArray.Length; i++)
                    {
                        Debug.Write(equipmentArray[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Equipment
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteEquipment(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var equipment = db.masscombat_equipment.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_equipment.Remove(equipment);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Equipment
        /// <summary>
        /// Returns: ID of the Equipment or -1 when some Error occurs
        /// </summary>
        public static int UpdateEquipment(McEquipment mcEquipment)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var equipment = db.masscombat_equipment.SingleOrDefault(x => x.i_id == mcEquipment.Id);

                    equipment.vc_equipment = mcEquipment.Name;
                    equipment.i_melee = mcEquipment.Melee;
                    equipment.i_ranged = mcEquipment.Ranged;
                    equipment.i_defensive = mcEquipment.Defensive;
                    equipment.fl_health = mcEquipment.Health;
                    equipment.fl_movement_strategically = mcEquipment.MovementStrategically;
                    equipment.i_movement_tactical = mcEquipment.MovementTactical;
                    equipment.i_morale = mcEquipment.Morale;
                    equipment.i_fortitude = mcEquipment.Fortitude;
                    equipment.i_reflex = mcEquipment.Reflex;
                    equipment.fl_upkeep = mcEquipment.Upkeep;
                    db.SaveChanges();

                    return equipment.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Factions 
        #region Read Factions 
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McFaction; null if no valid Factions are found
        /// </summary>
        public static McFaction[] ReadFactions(int[] ids)
        {
            McFaction[] factions;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        factions = new McFaction[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var faction = db.masscombat_faction.SingleOrDefault(x => x.i_id == id);
                            factions[0] = new McFaction(faction.i_id, faction.vc_faction);
                        }
                    }
                    else // all IDs
                    {
                        List<McFaction> factionList = new List<McFaction>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_faction
                                        orderby b.i_id
                                        select b;

                            foreach (var faction in query)
                            {
                                factionList.Add(new McFaction(faction.i_id, faction.vc_faction));
                            }
                        }
                        factions = factionList.ToArray();
                    }
                }
                else // Several IDs
                {
                    factions = new McFaction[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var faction = db.masscombat_faction.SingleOrDefault(x => x.i_id == id);
                            factions[i] = new McFaction(faction.i_id, faction.vc_faction);
                        }
                    }
                }
                return factions;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Factions
        /// <summary>
        /// Params: 1 or more McFaction
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteFactions(McFaction[] factions)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < factions.Length; i++)
                    {
                        var faction = new masscombat_faction { vc_faction = factions[i].Name };
                        db.masscombat_faction.Add(faction);
                        db.SaveChanges();

                        WrittenIds.Add(faction.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (factions.Length == 0)
                {
                    Debug.Write("No Faction");
                }
                else
                {
                    Debug.Write("factions: ");
                    for (int i = 0; i < factions.Length; i++)
                    {
                        Debug.Write(factions[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Factions
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteFactions(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var faction = db.masscombat_faction.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_faction.Remove(faction);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Faction
        /// <summary>
        /// Returns: ID of the Faction or -1 when some Error occurs
        /// </summary>
        public static int UpdateFaction(McFaction mcFaction)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var faction = db.masscombat_faction.SingleOrDefault(x => x.i_id == mcFaction.Id);

                    faction.vc_faction = mcFaction.Name;
                    db.SaveChanges();

                    return faction.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Leadership Roles 
        #region Read Leadership Roles 
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McLeadershipRole; null if no valid LeadershipRoles are found
        /// </summary>
        public static McLeadershipRole[] ReadLeadershipRoles(int[] ids)
        {
            McLeadershipRole[] leadershipRoles;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        leadershipRoles = new McLeadershipRole[1];
                        using (var db = new MasscombatContext())
                        {
                            var leadershipRole = db.masscombat_leadership_role_command.SingleOrDefault(x => x.i_id == ids[0]);
                            leadershipRoles[0] = new McLeadershipRole(leadershipRole.i_id, leadershipRole.vc_leadership_role, leadershipRole.b_main_department, leadershipRole.i_command_id);
                        }
                    }
                    else // all IDs
                    {
                        List<McLeadershipRole> leadershipRoleList = new List<McLeadershipRole>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_leadership_role_command
                                        orderby b.i_id
                                        select b;

                            foreach (var leadershipRole in query)
                            {
                                leadershipRoleList.Add(new McLeadershipRole(leadershipRole.i_id, leadershipRole.vc_leadership_role, leadershipRole.b_main_department, leadershipRole.i_command_id));
                            }
                        }
                        leadershipRoles = leadershipRoleList.ToArray();
                    }
                }
                else // Several IDs
                {
                    leadershipRoles = new McLeadershipRole[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var leadershipRole = db.masscombat_leadership_role_command.SingleOrDefault(x => x.i_id == id);
                            leadershipRoles[i] = new McLeadershipRole(leadershipRole.i_id, leadershipRole.vc_leadership_role, leadershipRole.b_main_department, leadershipRole.i_command_id);
                        }
                    }
                }
                return leadershipRoles;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion
        #endregion

        #region Levels 
        #region Read Levels
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McLevel; null if no valid Levels are found
        /// </summary>
        public static McLevel[] ReadLevels(int[] ids)
        {
            McLevel[] levels;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        levels = new McLevel[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var level = db.masscombat_level.SingleOrDefault(x => x.i_id == id);
                            levels[0] = new McLevel(level.i_id, level.vc_level, level.i_melee, level.i_ranged, level.i_defensive, level.fl_health, level.fl_movement_strategically, level.i_movement_tactical, level.i_morale, level.i_fortitude, level.i_reflex, level.fl_value, level.fl_upkeep, level.i_threat, level.i_xp_needed);
                        }
                    }
                    else // all IDs
                    {
                        List<McLevel> LevelList = new List<McLevel>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_level
                                        orderby b.i_id
                                        select b;

                            foreach (var level in query)
                            {
                                LevelList.Add(new McLevel(level.i_id, level.vc_level, level.i_melee, level.i_ranged, level.i_defensive, level.fl_health, level.fl_movement_strategically, level.i_movement_tactical, level.i_morale, level.i_fortitude, level.i_reflex, level.fl_value, level.fl_upkeep, level.i_threat, level.i_xp_needed));
                            }
                        }
                        levels = LevelList.ToArray();
                    }
                }
                else // Several IDs
                {
                    levels = new McLevel[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var level = db.masscombat_level.SingleOrDefault(x => x.i_id == id);
                            levels[i] = new McLevel(level.i_id, level.vc_level, level.i_melee, level.i_ranged, level.i_defensive, level.fl_health, level.fl_movement_strategically, level.i_movement_tactical, level.i_morale, level.i_fortitude, level.i_reflex, level.fl_value, level.fl_upkeep, level.i_threat, level.i_xp_needed);
                        }
                    }
                }
                return levels;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Levels
        /// <summary>
        /// Params: 1 or more McLevel
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteLevels(McLevel[] levels)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < levels.Length; i++)
                    {
                        var level = new masscombat_level { vc_level = levels[i].Name, i_melee = levels[i].Melee, i_ranged = levels[i].Ranged, i_defensive = levels[i].Defensive, fl_health = levels[i].Health, fl_movement_strategically = levels[i].MovementStrategically, i_movement_tactical = levels[i].MovementTactical, i_morale = levels[i].Morale, i_fortitude = levels[i].Fortitude, i_reflex = levels[i].Reflex, fl_value = levels[i].Value, fl_upkeep = levels[i].Upkeep, i_threat = levels[i].Threat, i_xp_needed = levels[i].XpNeeded };
                        db.masscombat_level.Add(level);
                        db.SaveChanges();

                        WrittenIds.Add(level.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (levels.Length == 0)
                {
                    Debug.Write("No Level");
                }
                else
                {
                    Debug.Write("Levels: ");
                    for (int i = 0; i < levels.Length; i++)
                    {
                        Debug.Write(levels[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Levels
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteLevels(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var level = db.masscombat_level.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_level.Remove(level);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Level
        /// <summary>
        /// Returns: ID of the Level or -1 when some Error occurs
        /// </summary>
        public static int UpdateLevel(McLevel mcLevel)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var level = db.masscombat_level.SingleOrDefault(x => x.i_id == mcLevel.Id);

                    level.vc_level = mcLevel.Name;
                    level.i_melee = mcLevel.Melee;
                    level.i_ranged = mcLevel.Ranged;
                    level.i_defensive = mcLevel.Defensive;
                    level.fl_health = mcLevel.Health;
                    level.fl_movement_strategically = mcLevel.MovementStrategically;
                    level.i_movement_tactical = mcLevel.MovementTactical;
                    level.i_morale = mcLevel.Morale;
                    level.i_fortitude = mcLevel.Fortitude;
                    level.i_reflex = mcLevel.Reflex;
                    level.fl_value = mcLevel.Value;
                    level.fl_upkeep = mcLevel.Upkeep;
                    level.i_threat = mcLevel.Threat;
                    level.i_xp_needed = mcLevel.XpNeeded;
                    db.SaveChanges();

                    return level.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Promotions 
        #region Read Promotions
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McPromotion; null if no valid Promotions are found
        /// </summary>
        public static McPromotion[] ReadPromotions(int[] ids)
        {
            McPromotion[] promotions;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        promotions = new McPromotion[1];
                        using (var db = new MasscombatContext())
                        {
                            var promotion = db.masscombat_promotion.SingleOrDefault(x => x.i_id == ids[0]);
                            promotions[0] = new McPromotion(promotion.i_id, promotion.vc_promotion, promotion.i_melee, promotion.i_ranged, promotion.i_defensive, promotion.fl_health, promotion.fl_movement_strategically, promotion.i_movement_tactical, promotion.i_morale, promotion.i_fortitude, promotion.i_reflex, promotion.fl_value, promotion.fl_upkeep, promotion.vc_description, promotion.vc_tooltip);
                        }
                    }
                    else // all IDs
                    {
                        List<McPromotion> promotionList = new List<McPromotion>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_promotion
                                        orderby b.i_id
                                        select b;

                            foreach (var promotion in query)
                            {
                                promotionList.Add(new McPromotion(promotion.i_id, promotion.vc_promotion, promotion.i_melee, promotion.i_ranged, promotion.i_defensive, promotion.fl_health, promotion.fl_movement_strategically, promotion.i_movement_tactical, promotion.i_morale, promotion.i_fortitude, promotion.i_reflex, promotion.fl_value, promotion.fl_upkeep, promotion.vc_description, promotion.vc_tooltip));
                            }
                        }
                        promotions = promotionList.ToArray();
                    }
                }
                else // Several IDs
                {
                    promotions = new McPromotion[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var promotion = db.masscombat_promotion.SingleOrDefault(x => x.i_id == id);
                            promotions[i] = new McPromotion(promotion.i_id, promotion.vc_promotion, promotion.i_melee, promotion.i_ranged, promotion.i_defensive, promotion.fl_health, promotion.fl_movement_strategically, promotion.i_movement_tactical, promotion.i_morale, promotion.i_fortitude, promotion.i_reflex, promotion.fl_value, promotion.fl_upkeep, promotion.vc_description, promotion.vc_tooltip);
                        }
                    }
                }
                return promotions;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Promotions
        /// <summary>
        /// Params: 1 or more McPromotion
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WritePromotions(McPromotion[] promotions)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < promotions.Length; i++)
                    {
                        var promotion = new masscombat_promotion { vc_promotion = promotions[i].Name, i_melee = promotions[i].Melee, i_ranged = promotions[i].Ranged, i_defensive = promotions[i].Defensive, fl_health = promotions[i].Health, fl_movement_strategically = promotions[i].MovementStrategically, i_movement_tactical = promotions[i].MovementTactical, i_morale = promotions[i].Morale, i_fortitude = promotions[i].Fortitude, i_reflex = promotions[i].Reflex, fl_value = promotions[i].Value, fl_upkeep = promotions[i].Upkeep, vc_description = promotions[i].Description, vc_tooltip = promotions[i].Tooltip };
                        db.masscombat_promotion.Add(promotion);
                        db.SaveChanges();

                        WrittenIds.Add(promotion.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (promotions.Length == 0)
                {
                    Debug.Write("No Promotion");
                }
                else
                {
                    Debug.Write("Promotions: ");
                    for (int i = 0; i < promotions.Length; i++)
                    {
                        Debug.Write(promotions[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Abilities
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeletePromotions(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var promotion = db.masscombat_promotion.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_promotion.Remove(promotion);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Promotion
        /// <summary>
        /// Returns: ID of the Promotion or -1 when some Error occurs
        /// </summary>
        public static int UpdatePromotion(McPromotion mcPromotion)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var promotion = db.masscombat_promotion.SingleOrDefault(x => x.i_id == mcPromotion.Id);

                    promotion.vc_promotion = mcPromotion.Name;
                    promotion.i_melee = mcPromotion.Melee;
                    promotion.i_ranged = mcPromotion.Ranged;
                    promotion.i_defensive = mcPromotion.Defensive;
                    promotion.fl_health = mcPromotion.Health;
                    promotion.fl_movement_strategically = mcPromotion.MovementStrategically;
                    promotion.i_movement_tactical = mcPromotion.MovementTactical;
                    promotion.i_morale = mcPromotion.Morale;
                    promotion.i_fortitude = mcPromotion.Fortitude;
                    promotion.i_reflex = mcPromotion.Reflex;
                    promotion.fl_value = mcPromotion.Value;
                    promotion.fl_upkeep = mcPromotion.Upkeep;
                    promotion.vc_description = mcPromotion.Description;
                    promotion.vc_tooltip = mcPromotion.Tooltip;
                    db.SaveChanges();

                    return promotion.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Races 
        #region Read Races
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McRace; null if no valid Races are found
        /// </summary>
        public static McRace[] ReadRaces(int[] ids)
        {
            McRace[] races;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        races = new McRace[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var race = db.masscombat_race.SingleOrDefault(x => x.i_id == id);
                            races[0] = new McRace(race.i_id, race.vc_race, race.i_kind, race.i_melee, race.i_ranged, race.i_defensive, race.fl_health, race.fl_movement_strategically, race.i_movement_tactical, race.i_morale, race.i_fortitude, race.i_reflex, race.fl_value, race.fl_upkeep, race.i_threat, race.b_mount, race.i_charisma_base);
                        }
                    }
                    else // all IDs
                    {
                        List<McRace> raceList = new List<McRace>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_race
                                        orderby b.i_id
                                        select b;

                            foreach (var race in query)
                            {
                                raceList.Add(new McRace(race.i_id, race.vc_race, race.i_kind, race.i_melee, race.i_ranged, race.i_defensive, race.fl_health, race.fl_movement_strategically, race.i_movement_tactical, race.i_morale, race.i_fortitude, race.i_reflex, race.fl_value, race.fl_upkeep, race.i_threat, race.b_mount, race.i_charisma_base));
                            }
                        }
                        races = raceList.ToArray();
                    }
                }
                else // Several IDs
                {
                    races = new McRace[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var race = db.masscombat_race.SingleOrDefault(x => x.i_id == id);
                            races[i] = new McRace(race.i_id, race.vc_race, race.i_kind, race.i_melee, race.i_ranged, race.i_defensive, race.fl_health, race.fl_movement_strategically, race.i_movement_tactical, race.i_morale, race.i_fortitude, race.i_reflex, race.fl_value, race.fl_upkeep, race.i_threat, race.b_mount, race.i_charisma_base);
                        }
                    }
                }
                return races;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Races
        /// <summary>
        /// Params: 1 or more McRace
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] Writeraces(McRace[] races)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < races.Length; i++)
                    {
                        var race = new masscombat_race { vc_race = races[i].Name, i_kind = races[i].Kind, i_melee = races[i].Melee, i_ranged = races[i].Ranged, i_defensive = races[i].Defensive, fl_health = races[i].Health, fl_movement_strategically = races[i].MovementStrategically, i_movement_tactical = races[i].MovementTactical, i_morale = races[i].Morale, i_fortitude = races[i].Fortitude, i_reflex = races[i].Reflex, fl_value = races[i].Value, fl_upkeep = races[i].Upkeep, i_threat = races[i].Threat, b_mount = races[i].IsMount, i_charisma_base = races[i].CharismaBase };
                        db.masscombat_race.Add(race);
                        db.SaveChanges();

                        WrittenIds.Add(race.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (races.Length == 0)
                {
                    Debug.Write("No Race");
                }
                else
                {
                    Debug.Write("Races: ");
                    for (int i = 0; i < races.Length; i++)
                    {
                        Debug.Write(races[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Races
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteRaces(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var race = db.masscombat_race.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_race.Remove(race);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Race
        /// <summary>
        /// Returns: ID of the race or -1 when some Error occurs
        /// </summary>
        public static int UpdateRace(McRace mcRace)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var race = db.masscombat_race.SingleOrDefault(x => x.i_id == mcRace.Id);

                    race.vc_race = mcRace.Name;
                    race.i_kind = mcRace.Kind;
                    race.i_melee = mcRace.Melee;
                    race.i_ranged = mcRace.Ranged;
                    race.i_defensive = mcRace.Defensive;
                    race.fl_health = mcRace.Health;
                    race.fl_movement_strategically = mcRace.MovementStrategically;
                    race.i_movement_tactical = mcRace.MovementTactical;
                    race.i_morale = mcRace.Morale;
                    race.i_fortitude = mcRace.Fortitude;
                    race.i_reflex = mcRace.Reflex;
                    race.fl_value = mcRace.Value;
                    race.fl_upkeep = mcRace.Upkeep;
                    race.i_threat = mcRace.Threat;
                    race.b_mount = mcRace.IsMount;
                    race.i_charisma_base = mcRace.CharismaBase;
                    db.SaveChanges();

                    return race.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Sizes 
        #region Read Sizes
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McSize; null if no valid Sizes are found
        /// </summary>
        public static McSize[] ReadSizes(int[] ids)
        {
            McSize[] sizes;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        sizes = new McSize[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var size = db.masscombat_size.SingleOrDefault(x => x.i_id == id);
                            sizes[0] = new McSize(size.i_id, size.vc_size, size.i_melee, size.i_ranged, size.i_defensive, size.i_health, size.fl_movement_strategically, size.i_movement_tactical, size.i_morale, size.i_fortitude, size.i_reflex, size.i_value, size.fl_upkeep, size.i_threat_bonus, size.i_squares);
                        }
                    }
                    else // all IDs
                    {
                        List<McSize> sizeList = new List<McSize>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_size
                                        orderby b.i_id
                                        select b;

                            foreach (var size in query)
                            {
                                sizeList.Add(new McSize(size.i_id, size.vc_size, size.i_melee, size.i_ranged, size.i_defensive, size.i_health, size.fl_movement_strategically, size.i_movement_tactical, size.i_morale, size.i_fortitude, size.i_reflex, size.i_value, size.fl_upkeep, size.i_threat_bonus, size.i_squares));
                            }
                        }
                        sizes = sizeList.ToArray();
                    }
                }
                else // Several IDs
                {
                    sizes = new McSize[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var size = db.masscombat_size.SingleOrDefault(x => x.i_id == id);
                            sizes[i] = new McSize(size.i_id, size.vc_size, size.i_melee, size.i_ranged, size.i_defensive, size.i_health, size.fl_movement_strategically, size.i_movement_tactical, size.i_morale, size.i_fortitude, size.i_reflex, size.i_value, size.fl_upkeep, size.i_threat_bonus, size.i_squares);
                        }
                    }
                }
                return sizes;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Sizes
        /// <summary>
        /// Params: 1 or more McSize
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteSizes(McSize[] sizes)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < sizes.Length; i++)
                    {
                        var size = new masscombat_size { vc_size = sizes[i].Name, i_melee = sizes[i].Melee, i_ranged = sizes[i].Ranged, i_defensive = sizes[i].Defensive, i_health = sizes[i].Health, fl_movement_strategically = sizes[i].MovementStrategically, i_movement_tactical = sizes[i].MovementTactical, i_morale = sizes[i].Morale, i_fortitude = sizes[i].Fortitude, i_reflex = sizes[i].Reflex, i_value = sizes[i].Value, fl_upkeep = sizes[i].Upkeep, i_threat_bonus = sizes[i].ThreatBonus, i_squares = sizes[i].Squares };
                        db.masscombat_size.Add(size);
                        db.SaveChanges();

                        WrittenIds.Add(size.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (sizes.Length == 0)
                {
                    Debug.Write("No Size");
                }
                else
                {
                    Debug.Write("Sizes: ");
                    for (int i = 0; i < sizes.Length; i++)
                    {
                        Debug.Write(sizes[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Sizes
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteSizes(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var size = db.masscombat_size.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_size.Remove(size);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Size
        /// <summary>
        /// Returns: ID of the Size or -1 when some Error occurs
        /// </summary>
        public static int UpdateSize(McSize mcSize)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var size = db.masscombat_size.SingleOrDefault(x => x.i_id == mcSize.Id);

                    size.vc_size = mcSize.Name;
                    size.i_melee = mcSize.Melee;
                    size.i_ranged = mcSize.Ranged;
                    size.i_defensive = mcSize.Defensive;
                    size.i_health = mcSize.Health;
                    size.fl_movement_strategically = mcSize.MovementStrategically;
                    size.i_movement_tactical = mcSize.MovementTactical;
                    size.i_morale = mcSize.Morale;
                    size.i_fortitude = mcSize.Fortitude;
                    size.i_reflex = mcSize.Reflex;
                    size.i_value = mcSize.Value;
                    size.fl_upkeep = mcSize.Upkeep;
                    size.i_threat_bonus = mcSize.ThreatBonus;
                    size.i_squares = mcSize.Squares;
                    db.SaveChanges();

                    return size.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Specializations 
        #region Read Specializations
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McAbility; null if no valid Abilities are found
        /// </summary>
        public static McSpecialization[] ReadSpecializations(int[] ids)
        {
            McSpecialization[] specializations;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        specializations = new McSpecialization[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var specialization = db.masscombat_specialization.SingleOrDefault(x => x.i_id == id);
                            specializations[0] = new McSpecialization(specialization.i_id, specialization.vc_specialization, specialization.i_melee, specialization.i_ranged, specialization.i_defensive, specialization.fl_health, specialization.fl_movement_strategically, specialization.i_movement_tactical, specialization.i_morale, specialization.i_fortitude, specialization.i_reflex, specialization.fl_value, specialization.fl_upkeep, specialization.t_description);
                        }
                    }
                    else // all IDs
                    {
                        List<McSpecialization> specializationList = new List<McSpecialization>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_specialization
                                        orderby b.i_id
                                        select b;

                            foreach (var specialization in query)
                            {
                                specializationList.Add(new McSpecialization(specialization.i_id, specialization.vc_specialization, specialization.i_melee, specialization.i_ranged, specialization.i_defensive, specialization.fl_health, specialization.fl_movement_strategically, specialization.i_movement_tactical, specialization.i_morale, specialization.i_fortitude, specialization.i_reflex, specialization.fl_value, specialization.fl_upkeep, specialization.t_description));
                            }
                        }
                        specializations = specializationList.ToArray();
                    }
                }
                else // Several IDs
                {
                    specializations = new McSpecialization[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var specialization = db.masscombat_specialization.SingleOrDefault(x => x.i_id == id);
                            specializations[i] = new McSpecialization(specialization.i_id, specialization.vc_specialization, specialization.i_melee, specialization.i_ranged, specialization.i_defensive, specialization.fl_health, specialization.fl_movement_strategically, specialization.i_movement_tactical, specialization.i_morale, specialization.i_fortitude, specialization.i_reflex, specialization.fl_value, specialization.fl_upkeep, specialization.t_description);
                        }
                    }
                }
                return specializations;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Specializtions
        /// <summary>
        /// Params: 1 or more McSpecialization
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteSpecializations(McSpecialization[] specializations)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < specializations.Length; i++)
                    {
                        var specialization = new masscombat_specialization { vc_specialization = specializations[i].Name, i_melee = specializations[i].Melee, i_ranged = specializations[i].Ranged, i_defensive = specializations[i].Defensive, fl_health = specializations[i].Health, fl_movement_strategically = specializations[i].MovementStrategically, i_movement_tactical = specializations[i].MovementTactical, i_morale = specializations[i].Morale, i_fortitude = specializations[i].Fortitude, i_reflex = specializations[i].Reflex, fl_value = specializations[i].Value, fl_upkeep = specializations[i].Upkeep, t_description = specializations[i].Description };
                        db.masscombat_specialization.Add(specialization);
                        db.SaveChanges();

                        WrittenIds.Add(specialization.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (specializations.Length == 0)
                {
                    Debug.Write("No Specialization");
                }
                else
                {
                    Debug.Write("Specializations: ");
                    for (int i = 0; i < specializations.Length; i++)
                    {
                        Debug.Write(specializations[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Abilities
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteSpecializations(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var specialization = db.masscombat_specialization.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_specialization.Remove(specialization);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Ability
        /// <summary>
        /// Returns: ID of the Ability or -1 when some Error occurs
        /// </summary>
        public static int UpdateSpecialization(McSpecialization mcSpecialization)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var specialization = db.masscombat_specialization.SingleOrDefault(x => x.i_id == mcSpecialization.Id);

                    specialization.vc_specialization = mcSpecialization.Name;
                    specialization.i_melee = mcSpecialization.Melee;
                    specialization.i_ranged = mcSpecialization.Ranged;
                    specialization.i_defensive = mcSpecialization.Defensive;
                    specialization.fl_health = mcSpecialization.Health;
                    specialization.fl_movement_strategically = mcSpecialization.MovementStrategically;
                    specialization.i_movement_tactical = mcSpecialization.MovementTactical;
                    specialization.i_morale = mcSpecialization.Morale;
                    specialization.i_fortitude = mcSpecialization.Fortitude;
                    specialization.i_reflex = mcSpecialization.Reflex;
                    specialization.fl_value = mcSpecialization.Value;
                    specialization.fl_upkeep = mcSpecialization.Upkeep;
                    specialization.t_description = mcSpecialization.Description;
                    db.SaveChanges();

                    return specialization.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Tactics 
        #region Read Tactics
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McTactic; null if no valid Tactics are found
        /// </summary>
        public static McTactic[] ReadTactics(int[] ids)
        {
            McTactic[] tactics;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        tactics = new McTactic[1];
                        using (var db = new MasscombatContext())
                        {
                            var tactic = db.masscombat_tactic.SingleOrDefault(x => x.i_id == ids[0]);
                            tactics[0] = new McTactic(tactic.i_id, tactic.vc_tactic, tactic.vc_description, tactic.vc_tooltip);
                        }
                    }
                    else // all IDs
                    {
                        List<McTactic> tacticList = new List<McTactic>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_tactic
                                        orderby b.i_id
                                        select b;

                            foreach (var tactic in query)
                            {
                                tacticList.Add(new McTactic(tactic.i_id, tactic.vc_tactic, tactic.vc_description, tactic.vc_tooltip));
                            }
                        }
                        tactics = tacticList.ToArray();
                    }
                }
                else // Several IDs
                {
                    tactics = new McTactic[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var tactic = db.masscombat_tactic.SingleOrDefault(x => x.i_id == id);
                            tactics[i] = new McTactic(tactic.i_id, tactic.vc_tactic, tactic.vc_description, tactic.vc_tooltip);
                        }
                    }
                }
                return tactics;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Tactics
        /// <summary>
        /// Params: 1 or more McTactic
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteTactics(McTactic[] tactics)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < tactics.Length; i++)
                    {
                        var tactic = new masscombat_tactic { vc_tactic = tactics[i].Name, vc_description = tactics[i].Description, vc_tooltip = tactics[i].Tooltip };
                        db.masscombat_tactic.Add(tactic);
                        db.SaveChanges();

                        WrittenIds.Add(tactic.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (tactics.Length == 0)
                {
                    Debug.Write("No Tactic");
                }
                else
                {
                    Debug.Write("Tactics: ");
                    for (int i = 0; i < tactics.Length; i++)
                    {
                        Debug.Write(tactics[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Tactics
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteTactics(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var tactic = db.masscombat_tactic.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_tactic.Remove(tactic);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Tactics
        /// <summary>
        /// Returns: ID of the Tactic or -1 when some Error occurs
        /// </summary>
        public static int UpdateTactic(McTactic mcTactic)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var tactic = db.masscombat_tactic.SingleOrDefault(x => x.i_id == mcTactic.Id);

                    tactic.vc_tactic = mcTactic.Name;
                    tactic.vc_description = mcTactic.Description;
                    tactic.vc_tooltip = mcTactic.Tooltip;
                    db.SaveChanges();

                    return tactic.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Types 
        #region Read Types
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McType; null if no valid Types are found
        /// </summary>
        public static McType[] ReadTypes(int[] ids)
        {
            McType[] types;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        types = new McType[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var type = db.masscombat_type.SingleOrDefault(x => x.i_id == id);
                            types[0] = new McType(type.i_id, type.vc_type, type.i_melee, type.i_ranged, type.i_defensive, type.fl_health, type.fl_movement_strategically, type.i_movement_tactical, type.i_morale, type.i_fortitude, type.i_reflex, type.fl_value, type.fl_upkeep);
                        }
                    }
                    else // all IDs
                    {
                        List<McType> typeList = new List<McType>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_type
                                        orderby b.i_id
                                        select b;

                            foreach (var type in query)
                            {
                                typeList.Add(new McType(type.i_id, type.vc_type, type.i_melee, type.i_ranged, type.i_defensive, type.fl_health, type.fl_movement_strategically, type.i_movement_tactical, type.i_morale, type.i_fortitude, type.i_reflex, type.fl_value, type.fl_upkeep));
                            }
                        }
                        types = typeList.ToArray();
                    }
                }
                else // Several IDs
                {
                    types = new McType[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var type = db.masscombat_type.SingleOrDefault(x => x.i_id == id);
                            types[i] = new McType(type.i_id, type.vc_type, type.i_melee, type.i_ranged, type.i_defensive, type.fl_health, type.fl_movement_strategically, type.i_movement_tactical, type.i_morale, type.i_fortitude, type.i_reflex, type.fl_value, type.fl_upkeep);
                        }
                    }
                }
                return types;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Types
        /// <summary>
        /// Params: 1 or more McType
        /// Returns: Array of all new IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] WriteTypes(McType[] types)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        var type = new masscombat_type { vc_type = types[i].Name, i_melee = types[i].Melee, i_ranged = types[i].Ranged, i_defensive = types[i].Defensive, fl_health = types[i].Health, fl_movement_strategically = types[i].MovementStrategically, i_movement_tactical = types[i].MovementTactical, i_morale = types[i].Morale, i_fortitude = types[i].Fortitude, i_reflex = types[i].Reflex, fl_value = types[i].Value, fl_upkeep = types[i].Upkeep };
                        db.masscombat_type.Add(type);
                        db.SaveChanges();

                        WrittenIds.Add(type.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (types.Length == 0)
                {
                    Debug.Write("No Type");
                }
                else
                {
                    Debug.Write("Types: ");
                    for (int i = 0; i < types.Length; i++)
                    {
                        Debug.Write(types[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Types
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteTypes(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var type = db.masscombat_type.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_type.Remove(type);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Type
        /// <summary>
        /// Returns: ID of the Type or -1 when some Error occurs
        /// </summary>
        public static int UpdateType(McType mcType)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var type = db.masscombat_type.SingleOrDefault(x => x.i_id == mcType.Id);

                    type.vc_type = mcType.Name;
                    type.i_melee = mcType.Melee;
                    type.i_ranged = mcType.Ranged;
                    type.i_defensive = mcType.Defensive;
                    type.fl_health = mcType.Health;
                    type.fl_movement_strategically = mcType.MovementStrategically;
                    type.i_movement_tactical = mcType.MovementTactical;
                    type.i_morale = mcType.Morale;
                    type.i_fortitude = mcType.Fortitude;
                    type.i_reflex = mcType.Reflex;
                    type.fl_value = mcType.Value;
                    type.fl_upkeep = mcType.Upkeep;
                    db.SaveChanges();

                    return type.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Units 
        #region Read Units 
        /// <summary>
        /// Params: 1 ID, several IDs or -1 for all IDs
        /// Returns: Array of all requested McUnit; null if no valid Units are found
        /// </summary>
        public static McUnit[] ReadUnits(int[] ids)
        {
            McUnit[] units;
            try
            {
                if (ids.Length == 1)
                {
                    if (ids[0] != -1) // 1 ID
                    {
                        units = new McUnit[1];
                        using (var db = new MasscombatContext())
                        {
                            int id = ids[0];
                            var unit = db.masscombat_unit.SingleOrDefault(x => x.i_id == id);
                            units[0] = new McUnit(unit.i_id, unit.vc_unit, unit.i_size_id, unit.i_type_id, unit.i_level_id, unit.i_xp, unit.i_specialization_id, unit.i_race_id, unit.i_faction_id, unit.i_commander_id, unit.t_icon, unit.i_mount_id);
                        }
                    }
                    else // all IDs
                    {
                        List<McUnit> unitList = new List<McUnit>();
                        using (var db = new MasscombatContext())
                        {
                            var query = from b in db.masscombat_unit
                                        orderby b.i_id
                                        select b;

                            foreach (var unit in query)
                            {
                                unitList.Add(new McUnit(unit.i_id, unit.vc_unit, unit.i_size_id, unit.i_type_id, unit.i_level_id, unit.i_xp, unit.i_specialization_id, unit.i_race_id, unit.i_faction_id, unit.i_commander_id, unit.t_icon, unit.i_mount_id));
                            }
                        }
                        units = unitList.ToArray();
                    }
                }
                else // Several IDs
                {
                    units = new McUnit[ids.Length];
                    using (var db = new MasscombatContext())
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            int id = ids[i];
                            var unit = db.masscombat_unit.SingleOrDefault(x => x.i_id == id);
                            units[i] = new McUnit(unit.i_id, unit.vc_unit, unit.i_size_id, unit.i_type_id, unit.i_level_id, unit.i_xp, unit.i_specialization_id, unit.i_race_id, unit.i_faction_id, unit.i_commander_id, unit.t_icon, unit.i_mount_id);
                        }
                    }
                }
                return units;
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }

        }
        #endregion

        #region Write Units
        /// <summary>
        /// Params: 1 or more McUnit
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteUnits(McUnit[] units)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < units.Length; i++)
                    {
                        var unit = new masscombat_unit { vc_unit = units[i].Name, i_size_id = units[i].SizeId, i_type_id = units[i].TypeId, i_level_id = units[i].LevelId, i_xp = units[i].Xp, i_specialization_id = units[i].SpecializationId, i_race_id = units[i].RaceId, i_faction_id = units[i].FactionId, i_commander_id = units[i].CommanderId, t_icon = units[i].IconString, i_mount_id = units[i].MountId };
                        db.masscombat_unit.Add(unit);
                        db.SaveChanges();

                        WrittenIds.Add(unit.i_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (units.Length == 0)
                {
                    Debug.Write("No Unit");
                }
                else
                {
                    Debug.Write("Units: ");
                    for (int i = 0; i < units.Length; i++)
                    {
                        Debug.Write(units[i].Name + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Units
        /// <summary>
        /// Params: 1 ID or more IDs
        /// Returns: Array of all deleted IDs and/or -1 if some Error occurs
        /// </summary>
        public static int[] DeleteUnits(int[] ids)
        {
            List<int> DeletedIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = ids[i];
                        var unit = db.masscombat_unit.SingleOrDefault(x => x.i_id == id);

                        db.masscombat_unit.Remove(unit);
                        db.SaveChanges();
                        DeletedIds.Add(ids[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ids.Length == 0)
                {
                    Debug.Write("No ID");
                }
                else
                {
                    Debug.Write("IDs: ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Debug.Write(ids[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                DeletedIds.Add(-1);
            }
            return DeletedIds.ToArray();
        }
        #endregion

        #region Update Unit
        /// <summary>
        /// Returns: ID of the Unit or -1 when some Error occurs
        /// </summary>
        public static int UpdateUnit(McUnit mcUnit)
        {
            try
            {
                using (var db = new MasscombatContext())
                {
                    var unit = db.masscombat_unit.SingleOrDefault(x => x.i_id == mcUnit.Id);

                    unit.vc_unit = mcUnit.Name;
                    unit.i_size_id = mcUnit.SizeId;
                    unit.i_type_id = mcUnit.TypeId;
                    unit.i_level_id = mcUnit.LevelId;
                    unit.i_xp = mcUnit.Xp;
                    unit.i_specialization_id = mcUnit.SpecializationId;
                    unit.i_race_id = mcUnit.RaceId;
                    unit.i_faction_id = mcUnit.FactionId;
                    unit.i_commander_id = mcUnit.CommanderId;
                    unit.t_icon = mcUnit.IconString;
                    unit.i_mount_id = mcUnit.MountId;
                    db.SaveChanges();

                    return unit.i_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return -1;
            }

        }
        #endregion
        #endregion

        #region Abilities to Race
        #region Read Abilities to Race
        /// <summary>
        /// Params: 1 Race-ID
        /// Returns: Array of all Abilities of the given Race
        /// </summary>
        public static int[] ReadAbilityIdsToRace(int raceId)
        {
            List<int> abilityIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_abilities_to_race.Where<masscombat_abilities_to_race>(x => x.i_race_id == raceId);



                    foreach (var item in query)
                    {
                        abilityIds.Add(item.i_ability_id);
                    }

                    return abilityIds.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.Write("Ability-IDs: ");
                foreach (int i in abilityIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Abilities to Race
        /// <summary>
        /// Params: 1 or more AbilityIds and 1 RaceID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteAbilitiesToRace(int[] abilityIds, int raceId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < abilityIds.Length; i++)
                    {
                        var a2r = new masscombat_abilities_to_race { i_ability_id = abilityIds[i], i_race_id = raceId };
                        db.masscombat_abilities_to_race.Add(a2r);
                        db.SaveChanges();

                        WrittenIds.Add(a2r.i_ability_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (abilityIds.Length == 0)
                {
                    Debug.Write("No Ability");
                }
                else
                {
                    Debug.Write("Abilities: ");
                    for (int i = 0; i < abilityIds.Length; i++)
                    {
                        Debug.Write(abilityIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Ability to Race
        /// <summary>
        /// Params: Ability-ID and Race-ID
        /// Returns: Array[0] = Deleted Ability-ID, Array[1] = Deleted Race-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteAbilityToRace(int abilityId, int raceId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_abilities_to_race.SingleOrDefault(x => (x.i_ability_id == abilityId && x.i_race_id == raceId));

                    db.masscombat_abilities_to_race.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_ability_id;
                    deletedIds[1] = item.i_race_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Abilities to Unit
        #region Read Abilities to Unit
        /// <summary>
        /// Params: 1 Unit-ID
        /// Returns: Array of all Abilities of the given Unit
        /// </summary>
        public static McAbility[] ReadAbilitiesToUnit(int unitId)
        {
            List<int> abilityIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_abilities_to_unit.Where<masscombat_abilities_to_unit>(x => x.i_unit_id == unitId);



                    foreach (var item in query)
                    {
                        abilityIds.Add(item.i_ability_id);
                    }

                    return ReadAbilities(abilityIds.ToArray());
                }
            }
            catch (Exception e)
            {
                Debug.Write("Ability-IDs: ");
                foreach (int i in abilityIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Abilities to Unit
        /// <summary>
        /// Params: 1 or more AbilityIds and 1 UnitID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteAbilitiesToUnit(int[] abilityIds, int unitId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < abilityIds.Length; i++)
                    {
                        var a2u = new masscombat_abilities_to_unit { i_ability_id = abilityIds[i], i_unit_id = unitId };
                        db.masscombat_abilities_to_unit.Add(a2u);
                        db.SaveChanges();

                        WrittenIds.Add(a2u.i_ability_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (abilityIds.Length == 0)
                {
                    Debug.Write("No Ability");
                }
                else
                {
                    Debug.Write("Abilities: ");
                    for (int i = 0; i < abilityIds.Length; i++)
                    {
                        Debug.Write(abilityIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Ability to Unit
        /// <summary>
        /// Params: Ability-ID and unit-ID
        /// Returns: Array[0] = Deleted Ability-ID, Array[1] = Deleted Unit-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteAbilityToUnit(int abilityId, int unitId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_abilities_to_unit.SingleOrDefault(x => (x.i_ability_id == abilityId && x.i_unit_id == unitId));

                    db.masscombat_abilities_to_unit.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_ability_id;
                    deletedIds[1] = item.i_unit_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Commands to Commander
        #region Read Commands to Commander
        /// <summary>
        /// Params: 1 Commander-ID
        /// Returns: Array of all Commands of the given Commander
        /// </summary>
        public static int[] ReadCommandIdsToCommander(int commanderId)
        {
            List<int> commandIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_commands_to_commander.Where<masscombat_commands_to_commander>(x => x.i_commander_id == commanderId);

                    foreach (var item in query)
                    {
                        commandIds.Add(item.i_command_id);
                    }

                    return commandIds.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.Write("Command-IDs: ");
                foreach (int i in commandIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Commands to Commander
        /// <summary>
        /// Params: 1 or more CommandIds and 1 CommanderID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteCommandsToCommander(int[] commandIds, int commanderId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < commandIds.Length; i++)
                    {
                        var c2c = new masscombat_commands_to_commander { i_command_id = commandIds[i], i_commander_id = commanderId };
                        db.masscombat_commands_to_commander.Add(c2c);
                        db.SaveChanges();

                        WrittenIds.Add(c2c.i_command_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (commandIds.Length == 0)
                {
                    Debug.Write("No Command");
                }
                else
                {
                    Debug.Write("Commands: ");
                    for (int i = 0; i < commandIds.Length; i++)
                    {
                        Debug.Write(commandIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Command to Commander
        /// <summary>
        /// Params: Command-ID and Commander-ID
        /// Returns: Array[0] = Deleted Command-ID, Array[1] = Deleted Commander-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteCommandToCommander(int commandId, int commanderId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_commands_to_commander.SingleOrDefault(x => (x.i_command_id == commandId && x.i_commander_id == commanderId));

                    db.masscombat_commands_to_commander.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_command_id;
                    deletedIds[1] = item.i_commander_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Equipment to Unit
        #region Read Equipment to Unit
        /// <summary>
        /// Params: 1 Unit-ID
        /// Returns: Array of all Equipment of the given Unit
        /// </summary>
        public static McEquipment[] ReadEquipmentToUnit(int unitId)
        {
            List<int> equipmentIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_equipment_to_unit.Where<masscombat_equipment_to_unit>(x => x.i_unit_id == unitId);

                    foreach (var item in query)
                    {
                        equipmentIds.Add(item.i_equipment_id);
                    }

                    return ReadEquipment(equipmentIds.ToArray());
                }
            }
            catch (Exception e)
            {
                Debug.Write("Equipment-IDs: ");
                foreach (int i in equipmentIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Equipment to Unit
        /// <summary>
        /// Params: 1 or more EquipmentIds and 1 UnitID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteEquipmentToUnit(int[] equipmentIds, int unitId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < equipmentIds.Length; i++)
                    {
                        var e2u = new masscombat_equipment_to_unit { i_equipment_id = equipmentIds[i], i_unit_id = unitId };
                        db.masscombat_equipment_to_unit.Add(e2u);
                        db.SaveChanges();

                        WrittenIds.Add(e2u.i_equipment_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (equipmentIds.Length == 0)
                {
                    Debug.Write("No Equipment");
                }
                else
                {
                    Debug.Write("Equipment: ");
                    for (int i = 0; i < equipmentIds.Length; i++)
                    {
                        Debug.Write(equipmentIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Equipment to Unit
        /// <summary>
        /// Params: Equipment-ID and unit-ID
        /// Returns: Array[0] = Deleted Equipment-ID, Array[1] = Deleted Unit-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteEquipmentToUnit(int equipmentId, int unitId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_equipment_to_unit.SingleOrDefault(x => (x.i_equipment_id == equipmentId && x.i_unit_id == unitId));

                    db.masscombat_equipment_to_unit.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_equipment_id;
                    deletedIds[1] = item.i_unit_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Equipment Costs
        #region Read Equipment Costs
        /// <summary>
        /// Params: 1 Equipment-ID
        /// Returns: Dictonary<int Size-ID, float cost>, Null on Error
        /// </summary>
        public static Dictionary<int, float> ReadEquipmentCost(int equipmentId)
        {
            Dictionary<int,float> costs = new Dictionary<int,float>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_equipment_costs.Where<masscombat_equipment_costs>(x => x.i_equipment_id == equipmentId);

                    foreach (var item in query)
                    {
                        costs.Add(item.i_unit_size_id,item.fl_costs);
                    }

                    return costs;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion
        #endregion

        #region Promotions to Race
        #region Read Promotions to Race
        /// <summary>
        /// Params: 1 Race-ID
        /// Returns: Array of all Promotion-IDs of the given Race
        /// </summary>
        public static int[] ReadPromotionIdsToRace(int raceId)
        {
            List<int> promotionIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_promotions_to_race.Where<masscombat_promotions_to_race>(x => x.i_race_id == raceId);

                    foreach (var item in query)
                    {
                        promotionIds.Add(item.i_promotion_id);
                    }

                    return promotionIds.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.Write("Promotion-IDs: ");
                foreach (int i in promotionIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Promotions to Race
        /// <summary>
        /// Params: 1 or more PromotionIds and 1 RaceID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WritePromotionsToRace(int[] promotionIds, int raceId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < promotionIds.Length; i++)
                    {
                        var p2r = new masscombat_promotions_to_race { i_promotion_id = promotionIds[i], i_race_id = raceId };
                        db.masscombat_promotions_to_race.Add(p2r);
                        db.SaveChanges();

                        WrittenIds.Add(p2r.i_promotion_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (promotionIds.Length == 0)
                {
                    Debug.Write("No Promotion");
                }
                else
                {
                    Debug.Write("Promotions: ");
                    for (int i = 0; i < promotionIds.Length; i++)
                    {
                        Debug.Write(promotionIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Promotion to Race
        /// <summary>
        /// Params: Promotion-ID and Race-ID
        /// Returns: Array[0] = Deleted Promotion-ID, Array[1] = Deleted Race-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeletePromotionToRace(int promotionId, int raceId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_promotions_to_race.SingleOrDefault(x => (x.i_promotion_id == promotionId && x.i_race_id == raceId));

                    db.masscombat_promotions_to_race.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_promotion_id;
                    deletedIds[1] = item.i_race_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Promotions to Unit
        #region Read Promotions to Unit
        /// <summary>
        /// Params: 1 Unit-ID
        /// Returns: Array of all Promotions of the given Unit
        /// </summary>
        public static McPromotion[] ReadPromotionsToUnit(int unitId)
        {
            List<int> promotionIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_promotions_to_unit.Where<masscombat_promotions_to_unit>(x => x.i_unit_id == unitId);

                    foreach (var item in query)
                    {
                        promotionIds.Add(item.i_promotion_id);
                    }

                    return ReadPromotions(promotionIds.ToArray());
                }
            }
            catch (Exception e)
            {
                Debug.Write("Promotion-IDs: ");
                foreach (int i in promotionIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Promotions to Unit
        /// <summary>
        /// Params: 1 or more PromotionIds and 1 UnitID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WritePromotionsToUnit(int[] promotionIds, int unitId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < promotionIds.Length; i++)
                    {
                        var p2u = new masscombat_promotions_to_unit { i_promotion_id = promotionIds[i], i_unit_id = unitId };
                        db.masscombat_promotions_to_unit.Add(p2u);
                        db.SaveChanges();

                        WrittenIds.Add(p2u.i_promotion_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (promotionIds.Length == 0)
                {
                    Debug.Write("No Promotion");
                }
                else
                {
                    Debug.Write("Promotions: ");
                    for (int i = 0; i < promotionIds.Length; i++)
                    {
                        Debug.Write(promotionIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Promotion to Unit
        /// <summary>
        /// Params: Promotion-ID and Unit-ID
        /// Returns: Array[0] = Deleted Promotion-ID, Array[1] = Deleted Unit-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeletePromotionToUnit(int promotionId, int unitId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_promotions_to_unit.SingleOrDefault(x => (x.i_promotion_id == promotionId && x.i_unit_id == unitId));

                    db.masscombat_promotions_to_unit.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_promotion_id;
                    deletedIds[1] = item.i_unit_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Tactics to Race
        #region Read Tactics to Race
        /// <summary>
        /// Params: 1 Race-ID
        /// Returns: Array of all Tactic-IDs of the given Race
        /// </summary>
        public static int[] ReadTacticIdsToRace(int raceId)
        {
            List<int> tacticIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_tactics_to_race.Where<masscombat_tactics_to_race>(x => x.i_race_id == raceId);

                    foreach (var item in query)
                    {
                        tacticIds.Add(item.i_tactic_id);
                    }

                    return tacticIds.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.Write("Tactic-IDs: ");
                foreach (int i in tacticIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Tactics to Race
        /// <summary>
        /// Params: 1 or more TacticIds and 1 RaceID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteTacticsToRace(int[] tacticIds, int raceId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < tacticIds.Length; i++)
                    {
                        var t2r = new masscombat_tactics_to_race { i_tactic_id = tacticIds[i], i_race_id = raceId };
                        db.masscombat_tactics_to_race.Add(t2r);
                        db.SaveChanges();

                        WrittenIds.Add(t2r.i_tactic_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (tacticIds.Length == 0)
                {
                    Debug.Write("No Tactic");
                }
                else
                {
                    Debug.Write("Tactics: ");
                    for (int i = 0; i < tacticIds.Length; i++)
                    {
                        Debug.Write(tacticIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Tactic to Race
        /// <summary>
        /// Params: Tactic-ID and Race-ID
        /// Returns: Array[0] = Deleted Tactic-ID, Array[1] = Deleted Race-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteTacticToRace(int tacticId, int raceId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_tactics_to_race.SingleOrDefault(x => (x.i_tactic_id == tacticId && x.i_race_id == raceId));

                    db.masscombat_tactics_to_race.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_tactic_id;
                    deletedIds[1] = item.i_race_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion

        #region Tactics to Unit
        #region Read Tactics to Unit
        /// <summary>
        /// Params: 1 Unit-ID
        /// Returns: Array of all Tactics of the given Unit
        /// </summary>
        public static McTactic[] ReadTacticsToUnit(int unitId)
        {
            List<int> tacticIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    var query = db.masscombat_tactics_to_unit.Where<masscombat_tactics_to_unit>(x => x.i_unit_id == unitId);

                    foreach (var item in query)
                    {
                        tacticIds.Add(item.i_tactic_id);
                    }

                    return ReadTactics(tacticIds.ToArray());
                }
            }
            catch (Exception e)
            {
                Debug.Write("Tactic-IDs: ");
                foreach (int i in tacticIds)
                {
                    Debug.Write(i + ", ");
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Write Tactics to Unit
        /// <summary>
        /// Params: 1 or more TacticIds and 1 UnitID
        /// Returns: Array of all new IDs; -1 if some Error occurs
        /// </summary>
        public static int[] WriteTacticsToUnit(int[] tacticIds, int unitId)
        {
            List<int> WrittenIds = new List<int>();
            try
            {
                using (var db = new MasscombatContext())
                {
                    for (int i = 0; i < tacticIds.Length; i++)
                    {
                        var t2u = new masscombat_tactics_to_unit { i_tactic_id = tacticIds[i], i_unit_id = unitId };
                        db.masscombat_tactics_to_unit.Add(t2u);
                        db.SaveChanges();

                        WrittenIds.Add(t2u.i_tactic_id);
                    }
                }
            }
            catch (Exception e)
            {
                if (tacticIds.Length == 0)
                {
                    Debug.Write("No Tactic");
                }
                else
                {
                    Debug.Write("Tactics: ");
                    for (int i = 0; i < tacticIds.Length; i++)
                    {
                        Debug.Write(tacticIds[i] + ", ");
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);
                WrittenIds.Add(-1);
            }
            return WrittenIds.ToArray();
        }
        #endregion

        #region Delete Tactic to Unit
        /// <summary>
        /// Params: Tactic-ID and Unit-ID
        /// Returns: Array[0] = Deleted Tactic-ID, Array[1] = Deleted Unit-ID, -1 on both in case of error
        /// </summary>
        public static int[] DeleteTacticToUnit(int tacticId, int unitId)
        {
            int[] deletedIds = new int[2];
            try
            {
                using (var db = new MasscombatContext())
                {
                    var item = db.masscombat_tactics_to_unit.SingleOrDefault(x => (x.i_tactic_id == tacticId && x.i_unit_id == unitId));

                    db.masscombat_tactics_to_unit.Remove(item);
                    db.SaveChanges();
                    deletedIds[0] = item.i_tactic_id;
                    deletedIds[1] = item.i_unit_id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:");
                Debug.WriteLine(e);

                deletedIds[0] = -1;
                deletedIds[1] = -1;
            }
            return deletedIds;
        }
        #endregion
        #endregion
    }

}
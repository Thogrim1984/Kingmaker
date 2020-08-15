using GSF.Collections;
using Kingmaker.DataBase.Masscombat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat
{
    public class McCache
    {
        public ReadOnlyObservableCollection<McAbility> AbilityList { get; set; }
        public ReadOnlyObservableCollection<McCommand> CommandList { get; set; }
        public ReadOnlyObservableCollection<McCommander> CommanderList { get; set; }
        public ReadOnlyObservableCollection<McEquipment> EquipmentList { get; set; }
        public ReadOnlyObservableCollection<McFaction> FactionList { get; set; }
        public ReadOnlyObservableCollection<McLeadershipRole> LeadershipRoleList { get; set; }
        public ReadOnlyObservableCollection<McLevel> LevelList { get; set; }
        public ReadOnlyObservableCollection<McPromotion> PromotionList { get; set; }
        public ReadOnlyObservableCollection<McRace> RaceList { get; set; }
        public ReadOnlyObservableCollection<McSize> SizeList { get; set; }
        public ReadOnlyObservableCollection<McSpecialization> SpezializationList { get; set; }
        public ReadOnlyObservableCollection<McTactic> TacticList { get; set; }
        public ReadOnlyObservableCollection<McType> TypeList { get; set; }
        public ReadOnlyObservableCollection<McUnit> UnitList { get; set; }

        public McCache()
        {
            McModelFactory mcModelFactory = new McModelFactory();

            AbilityList = new ReadOnlyObservableCollection<McAbility>(new ObservableCollection<McAbility>(mcModelFactory.getAbilities(new int[] { -1 })));
            CommandList = new ReadOnlyObservableCollection<McCommand>(new ObservableCollection<McCommand>( mcModelFactory.GetCommands(new int[] { -1 })));
            CommanderList = new ReadOnlyObservableCollection<McCommander>(new ObservableCollection<McCommander>( mcModelFactory.GetCommanders(new int[] { -1 })));
            EquipmentList = new ReadOnlyObservableCollection<McEquipment>(new ObservableCollection<McEquipment>( mcModelFactory.GetEquipment(new int[] { -1 })));
            FactionList = new ReadOnlyObservableCollection<McFaction>(new ObservableCollection<McFaction>( mcModelFactory.GetFactions(new int[] { -1 })));
            LeadershipRoleList = new ReadOnlyObservableCollection<McLeadershipRole>(new ObservableCollection<McLeadershipRole>( mcModelFactory.GetLeadershipRoles(new int[] { -1 })));
            LevelList = new ReadOnlyObservableCollection<McLevel>(new ObservableCollection<McLevel>( mcModelFactory.GetLevels(new int[] { -1 })));
            PromotionList = new ReadOnlyObservableCollection<McPromotion>(new ObservableCollection<McPromotion>( mcModelFactory.GetPromotions(new int[] { -1 })));
            RaceList = new ReadOnlyObservableCollection<McRace>(new ObservableCollection<McRace>( mcModelFactory.GetRaces(new int[] { -1 })));
            SizeList = new ReadOnlyObservableCollection<McSize>(new ObservableCollection<McSize>( mcModelFactory.GetSizes(new int[] { -1 })));
            SpezializationList = new ReadOnlyObservableCollection<McSpecialization>(new ObservableCollection<McSpecialization>( mcModelFactory.GetSpecializations(new int[] { -1 })));
            TacticList = new ReadOnlyObservableCollection<McTactic>(new ObservableCollection<McTactic>( mcModelFactory.GetTactics(new int[] { -1 })));
            TypeList = new ReadOnlyObservableCollection<McType>(new ObservableCollection<McType>( mcModelFactory.GetType(new int[] { -1 })));
            UnitList = new ReadOnlyObservableCollection<McUnit>(new ObservableCollection<McUnit>( mcModelFactory.GetUnits(new int[] { -1 })));
        }
    }
}

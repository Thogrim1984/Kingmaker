using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McCommand
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int CommandVariable { get; set; }
        public String Tooltip { get; set; }
        public String Description { get; set; }

        public McCommand()
        {
        }

        public McCommand(int id, string name, int commandVariable, string tooltip, string description)
        {
            Id = id;
            Name = name;
            CommandVariable = commandVariable;
            Tooltip = tooltip;
            Description = description;
        }

        public McCommand(string name, int commandVariable, string tooltip, string description)
        {
            Name = name;
            CommandVariable = commandVariable;
            Tooltip = tooltip;
            Description = description;
        }
    }
}

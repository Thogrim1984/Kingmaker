using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
   public  class McTactic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }

        public McTactic()
        {
        }

        public McTactic(string name, string description, string tooltip)
        {
            Name = name;
            Description = description;
            Tooltip = tooltip;
        }

        public McTactic(int id, string name, string description, string tooltip)
        {
            Id = id;
            Name = name;
            Description = description;
            Tooltip = tooltip;
        }
    }
}

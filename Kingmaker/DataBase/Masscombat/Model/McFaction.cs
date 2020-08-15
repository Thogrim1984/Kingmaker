using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McFaction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public McFaction(string name)
        {
            Name = name;
        }

        public McFaction()
        {
        }

        public McFaction(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

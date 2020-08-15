using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.DataBase.Masscombat.Model
{
    public class McLeadershipRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool MainDepartment { get; set; }
        public int CommandId { get; set; }

        public McLeadershipRole(string name, bool mainDepartment, int commandId)
        {
            Name = name;
            MainDepartment = mainDepartment;
            CommandId = commandId;
        }

        public McLeadershipRole(int id, string name, bool mainDepartment, int commandId)
        {
            Id = id;
            Name = name;
            MainDepartment = mainDepartment;
            CommandId = commandId;
        }
    }
}

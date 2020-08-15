using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker.Structure
{
    public class User
    {
        public String UserId { get; set; }
        public  String Name { get; set; }
        public  String PasswordHash { get; set; }

        public User()
        {
            UserId = Guid.NewGuid().ToString().Split('-')[4];
        }
    }
}

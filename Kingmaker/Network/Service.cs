using Kingmaker.DataBase.Masscombat.Model;
using Kingmaker.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kingmaker.Network
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "Service" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IServiceInbound
    {
        private IServiceCallback callback = null;
        private ObservableCollection<User> users;
        private readonly Dictionary<String, IServiceCallback> clients;

        public Service()
        {
            users = new ObservableCollection<User>();
            clients = new Dictionary<String, IServiceCallback>();
        }

        public void Connect(User user)
        {
            callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            if (callback != null)
            {
                clients.Add(user.UserId, callback);
                users.Add(user);
                clients?.ToList().ForEach(client => client.Value.UserConnected(users));                
            Debug.WriteLine($"{user.Name} just connected");
            }
        }

        public ObservableCollection<User> GetConnectedUsers()
        {
            return users;
        }

        public void SendUnit(McUnit unit)
        {
            Debug.WriteLine(unit.Name);
        }
    }
}

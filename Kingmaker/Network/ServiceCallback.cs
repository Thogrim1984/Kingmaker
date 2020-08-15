using Kingmaker.Structure;
using System.Collections.ObjectModel;

namespace Kingmaker.Network
{
    public class ServiceCallback : IServiceCallback
    {
        public ServiceCallback()
        {
        }

        public void UserConnected(ObservableCollection<User> users)
        {
            throw new System.NotImplementedException();
        }
    }
}
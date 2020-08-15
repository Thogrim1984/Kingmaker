using Kingmaker.Structure;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Kingmaker.Network
{
    
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserConnected(ObservableCollection<User> users);
    }
}

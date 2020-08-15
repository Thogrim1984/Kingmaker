using Kingmaker.DataBase.Masscombat.Model;
using Kingmaker.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kingmaker.Network
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
    public interface IServiceInbound
    {
        [OperationContract(IsOneWay = true)]
        void Connect(User user);
   
        [OperationContract(IsOneWay = true)]
        void SendUnit(McUnit unit);

        [OperationContract(IsOneWay = false)]
        ObservableCollection<User> GetConnectedUsers();
    }
}

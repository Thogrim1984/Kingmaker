

using Kingmaker.Structure;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows.Forms;

namespace Kingmaker
{
    static class Program
    {
        static int newestId;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExecuteExample();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }

        public static void ExecuteExample()
        {
            //using (var db = new MapContext())
            //{
            //    for (int i = 1; i < 10; i++)
            //    {
            //        for (int x = 1; x < 5; x++)
            //        {
            //            var lot = new map_district_ids { i_lot = x, i_block = i };
            //            db.map_district_ids.Add(lot);
            //            db.SaveChanges();
            //        }
            //    }
            //}

            //var uris = new Uri[1];
            //string adr = "net.tcp://localhost:8888/Service";
            //uris[0] = new Uri(adr);
            //IServiceInbound service = new Service();
            //ServiceHost host = new ServiceHost(service, uris);
            //var binding = new NetTcpBinding(SecurityMode.None);
            //host.AddServiceEndpoint(typeof(IServiceInbound), binding, "");
            //host.Opened += hostOnOpened;
            //host.Open();

            //User user = new User
            //{
            //    Name = "Heinz Peter"
            //};

            //var uri = "net.tcp://localhost:8888/Service";
            //var callback = new InstanceContext(new ServiceCallback());
            //var bindingClient = new NetTcpBinding(SecurityMode.None);
            //var channel = new DuplexChannelFactory<IServiceInbound>(callback, binding);
            //var endpoint = new EndpointAddress(uri);
            //var proxy = channel.CreateChannel(endpoint);

            ////proxy.Connect(user);

            //proxy.SendUnit(DBMasscombatCommunication.ReadUnits(new int[] { 9 })[0]);



            ////            //using ()
            ////            //{

            ////            //}
            //ServiceHost host = new ServiceHost(typeof(Service));
            //try
            //{
            //    // Open the ServiceHost to start listening for messages.
            //    host.Open();

            //    // The service can now be accessed.
            //    Console.WriteLine("Server: The service is ready.");

            //    // Close the ServiceHost.
            //    //host.Close();
            //}
            //catch (TimeoutException timeProblem)
            //{
            //    Console.WriteLine(timeProblem.Message);
            //    Console.ReadLine();
            //}
            //catch (CommunicationException commProblem)
            //{
            //    Console.WriteLine(commProblem.Message);
            //    Console.ReadLine();
            //}




            //DuplexChannelFactory<IServiceInbound> cF = new DuplexChannelFactory<IServiceInbound>("Client");

            //Debug.WriteLine(cF.Endpoint.Address);

            //IServiceInbound proxy = cF.CreateChannel();

            //User user = new User(/*"Peter Hans Dieter Schmidt", "Irgendein Passwort"*/);

            //Debug.WriteLine("Programm: " + user.Name + " " + user.UserId);

            //proxy.Connect(user);

            //ObservableCollection<User> users = proxy.GetConnectedUsers();



            //foreach (var item in users)
            //{
            //    Debug.WriteLine("Client: " + item.Name + " " + item.UserId);
            //}

        }
    

    private static void hostOnOpened(object sender, EventArgs e)
    {
        Debug.WriteLine("TCP Service started");
    }
}
}


using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;

namespace NONBAOHIEMVIETTIN.Hubs
{
    public class CounterHub : Hub
    {
        static long counter = 0;
        public override Task OnConnected()
        {
           
            var total = Convert.ToInt64(File.ReadAllText("../../../../../count.txt"));
            counter += 1;
            total += counter;
            File.WriteAllText("../count.txt", total.ToString());
            Clients.All.UpdateCount(counter);            
           Clients.All.UpdateTotal(total);
            return base.OnConnected();
        }     
        public override Task OnDisconnected(bool stopCalled)
        {
            counter -= 1;
            Clients.All.UpdateCount(counter);        
            return base.OnDisconnected(stopCalled);
        }
    }
}
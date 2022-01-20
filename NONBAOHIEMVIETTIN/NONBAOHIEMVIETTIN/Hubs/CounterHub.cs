using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using NONBAOHIEMVIETTIN.Models;
using System.Data.Entity;

namespace NONBAOHIEMVIETTIN.Hubs
{
    public class CounterHub : Hub
    {
        static long counter = 0;
        static long temp = 0;
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        public override Task OnConnected()
        {
            var countonline = db.countonline.FirstOrDefault();
            var total = countonline.total;

            counter += 1;
            if (temp < counter)
                total += 1;
            temp = counter;
            countonline.total = total;
            db.Entry(countonline).State = EntityState.Modified;
            db.SaveChanges();
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
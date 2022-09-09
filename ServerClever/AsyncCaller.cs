using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClever
{
    internal class AsyncCaller
    {
        private EventHandler _handler;
        public AsyncCaller(EventHandler handler)
        {
            _handler = handler;
        }

        public bool Invoke(int time, object sender, EventArgs args)
        {
            var task = Task.Factory.StartNew(() => _handler.Invoke(sender, args));
            return task.Wait(time);
        }
    }
}

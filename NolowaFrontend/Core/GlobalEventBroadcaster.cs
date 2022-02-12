using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core
{
    public class GlobalEventBroadcaster
    {
        private static readonly Lazy<GlobalEventBroadcaster> _instance = new Lazy<GlobalEventBroadcaster>(() => new GlobalEventBroadcaster());
        public static GlobalEventBroadcaster Instance => _instance.Value;

        private GlobalEventBroadcaster()
        {
                 
        }

        public void Register<T> (T eventType)
        {
        }
    }
}

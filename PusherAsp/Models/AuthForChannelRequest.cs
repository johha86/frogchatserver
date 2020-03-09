using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PusherAsp
{
    public class AuthForChannelRequest
    {
        public string channel_name { get; set; } 
        public string socket_id { get; set; }
    }
}

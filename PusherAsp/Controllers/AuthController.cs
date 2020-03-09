using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using PusherServer.Exceptions;
using PusherServer.RestfulClient;
using PusherServer.Util;

namespace PusherAsp.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class AuthController : ControllerBase
    {
        private readonly ChatContext m_context;
        public AuthController(ChatContext context)
        {
            m_context = context;
        }

        /// <summary>
        /// Autenticar a un usuario dentro del sistema
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginRequest data)
        {   
            //string user_name = "pepe";

            //if (user_name.Trim() == "")
            //{
            //    return Redirect("/");
            //}

            User user = m_context.Users.FirstOrDefault(u => u.name == data.username);

            if (user == null)
            {
                user = new User { name = data.username };

                m_context.Users.Add(user);
                m_context.SaveChanges();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("AuthForChannel")]
        public JsonResult AuthForChannel([FromBody] AuthForChannelRequest data)
        {
            var currentUser = m_context.Users.FirstOrDefault(u => u.name == "pepe");

            var options = new PusherOptions();
            options.Cluster = "us2";

            var pusher = new Pusher(
            "959730",
            "04016b8df0172af7d6fd",
            "0040d7c958bc43bc307a", options);

            if (data.channel_name.IndexOf(currentUser.id.ToString()) == -1)
            {
                return new JsonResult(
                  new { status = "error", message = "User cannot join channel" }
                );
            }
            
            var auth = pusher.Authenticate(data.channel_name, data.socket_id);
            
            return new JsonResult(auth);
        }
    }
}
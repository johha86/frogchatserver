using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PusherAsp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext m_context;

        public ChatController(ChatContext context)
        {
            m_context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //if (Session["user"] == null)
            //{
            //    return Redirect("/");
            //}
            
            //var currentUser = (Models.User)Session["user"];

            //using (var db = new Models.ChatContext())
            //{

            //    ViewBag.allUsers = db.Users.Where(u => u.name != currentUser.name)
            //                     .ToList();
            //}

            //ViewBag.currentUser = currentUser;

            //return View();
            return null;
        }

        /// <summary>
        /// Obtener todas las conversaciones para un usuario
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetUsersInConversation")]
        public ActionResult GetUsersInConversation()
        {            
            var foo = m_context.Users.Where(u => u.name != "pepe").ToList();
            return Ok(foo);
        }

        [HttpPost]
        [Route("ConversationWithContact")]
        public ActionResult ConversationWithContact(int contact)
        {
            var currentUser = m_context.Users.FirstOrDefault(u => u.name == "pepe");

            var conversations = new List<Conversation>();

            
                conversations = m_context.Conversations.
                                  Where(c => (c.receiver_id == currentUser.id
                                      && c.sender_id == contact) ||
                                      (c.receiver_id == contact
                                      && c.sender_id == currentUser.id))
                                  .OrderBy(c => c.created_at)
                                  .ToList();
            

            return Ok(
                new { status = "success", data = conversations }
            );
        }

        [HttpPost]
        [Route("SendMessage")]
        public ActionResult SendMessage()
        {
            var currentUser = m_context.Users.FirstOrDefault(u => u.name == "pepe");

            //string socket_id = Request.Form["socket_id"];

            Conversation convo = new Conversation
            {
                sender_id = currentUser.id,
                message = Request.Form["message"],
                receiver_id = Convert.ToInt32(Request.Form["contact"])
            };

            
            {
                m_context.Conversations.Add(convo);
                m_context.SaveChanges();
            }

            return Ok(convo);
        }
    }
}
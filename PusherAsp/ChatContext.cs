using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PusherAsp
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options):base(options)
        {
        }

        //public static ChatContext Create()
        //{
        //    return new ChatContext();
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}

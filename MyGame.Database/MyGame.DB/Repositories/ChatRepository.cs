using MyGame.DB.DB.Models.SystemChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class ChatRepository : IDisposable
    {
        public void Dispose()
        {

        }
        public Chat GetChatByLocatonId(Chat chat)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                chat = ctx.Chat.FirstOrDefault(c => c.LocationRefId == chat.LocationRefId);
                if (chat == null)
                    issues += "Could not find chat";
            }
            return chat;
        }
        public bool RemoveChat(Chat chat)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    ctx.Chat.Remove(chat);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    issues += "Issue removing chat";
                }
                if (issues == "")
                    return false;
                return true;
            }
        }
    }
}

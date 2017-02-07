using MyGame.DB.DB.Models.SystemChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class MessageRepository :IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public bool Update(Message message)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    var temp = ctx.Message.FirstOrDefault(m => m.MessageId == message.MessageId);
                    temp.Body = message.Body;
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
              

            }
            if (issues != "")
                return false;
            return true;
        }
        public Message GetById(Message message)
        {
            using (var ctx = new MyGameDBContext())
            {
                message = ctx.Message.FirstOrDefault(m => m.MessageId == message.MessageId);
            }
            return message;
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var ship = ctx.Ships.FirstOrDefault(t => t.ShipId == id);
                if (ship != null)
                {
                    ctx.Ships.Remove(ship);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool Add(Message message)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    ctx.Message.Add(message);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    issues += "Message could not be added";
                }


            }
            if (issues != "")
                return false;
            return true;
        }
    }
}

using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class ResearchRepository : IRepository<Research>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Research> GetAll()
        {
            var research = new List<Research>();

            using (var ctx = new MyGameDBContext())
            {
                research = ctx.Research
                     .ToList();
            }
            return research.AsQueryable();
        }
        public Research GetById(Guid id)
        {
            Research research;
            using (var ctx = new MyGameDBContext())
            {
                research = ctx.Research.FirstOrDefault(r => r.ResearchId == id);
            }
            return research;
        }
        public bool Delete(Research research)
        {
            return Delete(research.ResearchId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var research = ctx.Research.FirstOrDefault(t => t.ResearchId == id);
                if (research != null)
                {
                    ctx.Research.Remove(research);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Research research)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Research.FirstOrDefault(t => t.ResearchId == research.ResearchId);
                if (item != null)
                {
                    item.ResearchName = research.ResearchName;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Research research)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Research.Add(research);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
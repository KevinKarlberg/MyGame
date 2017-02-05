using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MyGame.DB.Repositories
{
    public class RacesRepository : IRepository<Races>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Races> GetAll()
        {
            var races = new List<Races>();

            using (var ctx = new MyGameDBContext())
            {
                races = ctx.Races
                     .Include(r => r.RaceId)
                     .Include(r => r.RaceName)
                     .ToList();
            }
            return races.AsQueryable();
        }
        public Races GetById(Guid id)
        {
            Races race;
            using (var ctx = new MyGameDBContext())
            {
                race = ctx.Races.Where(r => r.RaceId == id)
                    .Include(r => r.RaceId)
                    .Include(r => r.RaceName)
                    .FirstOrDefault();
            }
            return race;
        }
        public bool Delete(Races race)
        {
            return Delete(race.RaceId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var race = ctx.Races.Where(r => r.RaceId == id)
                       .Include(r => r.RaceId)
                       .Include(r => r.RaceName)
                       .FirstOrDefault();
                if (race != null)
                {
                    ctx.Races.Remove(race);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }

        }
        public void Update(Races race)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Races.Where(b => b.RaceId == race.RaceId).FirstOrDefault();
                if (item != null)
                {
                    item.RaceName = race.RaceName;
                }
            }
        }
        public void Add(Races race)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Races.Add(race);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

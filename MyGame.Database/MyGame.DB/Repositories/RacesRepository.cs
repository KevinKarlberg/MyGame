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
    public class RacesRepository : IRepository<Races>
    {
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
                var building = ctx.Buildings.Where(b => b.BuildingId == id)
                       .Include(b => b.BuildingId)
                       .Include(b => b.BuildingName)
                       .FirstOrDefault();
                if (building != null)
                {
                    ctx.Buildings.Remove(building);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }

        }
        public void Update(Buildings building)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Buildings.Where(b => b.BuildingId == building.BuildingId).FirstOrDefault();
                if (item != null)
                {
                    item.BuildingName = building.BuildingName;
                }
            }
        }
        public void Add(Buildings building)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Buildings.Add(building);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

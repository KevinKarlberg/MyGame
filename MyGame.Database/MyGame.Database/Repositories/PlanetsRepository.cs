using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyGame.Database.Repositories
{
    public class PlanetsRepository : IRepository<Planets>
    {
        private MyGameDBContext ctx;
        public PlanetsRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Planets> GetAll()
        {
            var planets = new List<Planets>();

            using (ctx)
            {
                planets = ctx.Planets
                     .Include(b => b.PlanetId)
                     .Include(b => b.PlanetName)
                     .ToList();
            }
            return planets.AsQueryable();
        }
        public Planets GetById(Guid id)
        {
            Planets planets;
            using (ctx)
            {
                planets = ctx.Planets.Where(b => b.PlanetId == id)
                    .Include(p => p.PlanetId)
                    .Include(p => p.PlanetName)
                    .Include(p => p.)
                    .FirstOrDefault();
            }
            return planets;
        }
        public bool Delete(Buildings building)
        {
            return Delete(building.BuildingId);
        }
        public bool Delete(Guid id)
        {
            using (ctx)
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
            using (ctx)
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
                using (ctx)
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
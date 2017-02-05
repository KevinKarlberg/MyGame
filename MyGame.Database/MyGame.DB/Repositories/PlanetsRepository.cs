using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyGame.DB.Repositories
{
    public class PlanetsRepository : IRepository<Planets>
    {
        public IQueryable<Planets> GetAll()
        {
            var planets = new List<Planets>();

            using (var ctx = new MyGameDBContext())
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
            Planets planet;
            using (var ctx = new MyGameDBContext())
            {
                planet = ctx.Planets.Where(b => b.PlanetId == id)
                    .Include(p => p.PlanetId)
                    .Include(p => p.PlanetName)
                    .Include(p => p.Terrain)
                    .FirstOrDefault();
            }
            return planet;
        }
        public bool Delete(Planets planet)
        {
            return Delete(planet.PlanetId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var planets = ctx.Planets.Where(p => p.PlanetId == id)
                       .Include(p => p.PlanetId)
                       .Include(b => b.PlanetName)
                       .Include(b => b.Terrain)
                       .FirstOrDefault();
                if (planets != null)
                {
                    ctx.Planets.Remove(planets);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }

        }
        public void Update(Planets planet)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Planets.Where(p => p.PlanetId == planet.PlanetId).FirstOrDefault();
                if (item != null)
                {
                    item.PlanetName = planet.PlanetName;
                    item.TerrainRefId = planet.TerrainRefId;
                    item.Terrain = planet.Terrain;
                }
            }
        }
        public void Add(Planets planet)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Planets.Add(planet);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
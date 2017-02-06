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
                     .ToList();
            }
            return planets.AsQueryable();
        }
        public Planets GetById(Guid id)
        {
            Planets planet;
            using (var ctx = new MyGameDBContext())
            {
                planet = ctx.Planets.FirstOrDefault(p => p.PlanetId == id);
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
                var planets = ctx.Planets.FirstOrDefault(p => p.PlanetId == id);
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
                    ctx.SaveChanges();
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
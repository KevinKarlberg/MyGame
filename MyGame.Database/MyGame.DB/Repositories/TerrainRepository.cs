using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyGame.DB;

namespace MyGame.Database.Repositories
{
    public class TerrainRepository : IRepository<Troops>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Terrain> GetAll()
        {
            var terrain = new List<Terrain>();

            using (var ctx = new MyGameDBContext())
            {
                terrain = ctx.Terrain
                     .ToList();
            }
            return terrain.AsQueryable();
        }
        public Terrain GetById(Guid id)
        {
            Terrain terrain;
            using (var ctx = new MyGameDBContext())
            {
                terrain = ctx.Terrain.FirstOrDefault(t => t.TerrainId == id);
            }
            return terrain;
        }
        public bool Delete(Terrain terrain)
        {
            return Delete(terrain.TerrainId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var terrain = ctx.Terrain.FirstOrDefault(t => t.TerrainId == id);
                if (terrain != null)
                {
                    ctx.Terrain.Remove(terrain);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Terrain terrain)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Terrain.FirstOrDefault(t => t.TerrainId == terrain.TerrainId);
                if (item != null)
                {
                    item.TerrainType = terrain.TerrainType;
                    item.TerrainDescription = terrain.TerrainDescription;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Terrain terrain)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Terrain.Add(terrain);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
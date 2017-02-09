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
    public class BuildingsRepository :IDisposable
    {
        public List<Buildings> GetAll()
        {
            var buildings = new List<Buildings>();

            using (var ctx = new MyGameDBContext())
            {
                buildings = ctx.Buildings
                 
                     .ToList();
            }
            return buildings;
        }
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public Buildings GetById(Guid id)
        {
            Buildings building;
            using (var ctx = new MyGameDBContext())
            {
                building = ctx.Buildings.FirstOrDefault(b => b.BuildingId == id);
            }
            return building;
        }
        public bool Delete(Buildings building)
        {
            return Delete(building.BuildingId);
        }
        public bool Delete(Guid id) 
        {
            using (var ctx = new MyGameDBContext())
            {
                var building = ctx.Buildings.FirstOrDefault(b => b.BuildingId == id);
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
                var item = ctx.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);
                if (item != null)
                {
                    item.BuildingName = building.BuildingName;
                    ctx.SaveChanges();
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
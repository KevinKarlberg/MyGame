using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.Database.Repositories
{
    public class BuildingsRepository : IRepository<Buildings>
    {
        private MyGameDBContext ctx;
        public BuildingsRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Buildings> GetAll()
        {
            throw new NotImplementedException();
        }
        public Buildings GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Buildings building)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Guid id) 
        {
            throw new NotImplementedException();
        }
        public void Update(Buildings building)
        {
            throw new NotImplementedException();
        }
        public void Add(Buildings building)
        {
            throw new NotImplementedException();
        }
    }
}
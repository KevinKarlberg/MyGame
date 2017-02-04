using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.Database.Repositories
{
    public class TroopsRepository : IRepository<Troops>
    {
        private MyGameDBContext ctx;
        public TroopsRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Troops> GetAll()
        {
            throw new NotImplementedException();
        }
        public Troops GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Troops troops)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public void Update(Troops troops)
        {
            throw new NotImplementedException();
        }
        public void Add(Troops troops)
        {
            throw new NotImplementedException();
        }
    }
}
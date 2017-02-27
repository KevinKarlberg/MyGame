using MYGame.DBv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private MyGameDBContext context = new MyGameDBContext();
        private GenericRepository<Players> playerRepo;
        private GenericRepository<MarketContent> marketContentRepository;

        public GenericRepository<Players> PlayerRepository
        {
            get
            {

                if (this.playerRepo == null)
                {
                    this.playerRepo = new GenericRepository<Players>(context);
                }
                return playerRepo;
            }
        }

        public GenericRepository<MarketContent> MarketContentRepository
        {
            get
            {

                if (this.marketContentRepository == null)
                {
                    this.marketContentRepository = new GenericRepository<MarketContent>(context);
                }
                return marketContentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

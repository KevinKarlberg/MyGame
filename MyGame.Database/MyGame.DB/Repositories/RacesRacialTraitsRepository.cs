using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
   public class RacesRacialTraitsRepository : IDisposable
    {
        public void Dispose()
        {
        }

        public List<RacesRacialTraits> GetAllByRace(Races race)
        {
            var list = new List<RacesRacialTraits>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.RacesRacialTraits.Where(r => r.RaceId == race.RaceId).ToList();
            }
            return list;
        }
    }
}

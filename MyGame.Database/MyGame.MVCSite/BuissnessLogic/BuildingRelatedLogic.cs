using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class BuildingRelatedLogic
    {
        public PlayerBuildings GeneratePlayerBuildingsForNewPlayer()
        {
            List<PlayerBuildings> pb = new List<PlayerBuildings>();
            using (var repo = new BuildingsRepository())
            {
                var list = repo.GetAll();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].BuildingName == "Residential")
                    {

                    }
                }
            }

        }
    }
}
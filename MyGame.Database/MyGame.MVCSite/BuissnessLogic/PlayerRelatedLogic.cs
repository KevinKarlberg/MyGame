using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class PlayerRelatedLogic
    {
        /// <summary>
        /// The clean version
        /// </summary>
        public void CreateNewPlayer()
        {
            Players player = PrivateCreateNewPlayer();
        }
        /// <summary>
        /// The messy private version
        /// </summary>
        private Players PrivateCreateNewPlayer()
        {
            Players player = new Players();
            player.PlayerId = Guid.NewGuid();

            // The stuff related to creating a starting planet 
            #region PlanetRelated
            // This is where the planet is created
            PlanetRelatedLogic p = new PlanetRelatedLogic();
            Planets planet = new Planets();
            planet = p.GeneratePlanetForNewPlayer();
            using (var repo = new PlanetsRepository())
            {
                repo.Add(planet);
            }
            PlayerPlanets pp = new PlayerPlanets();
            // This is where we add the planet and the player guid to the PlayerPlanet db
            pp.PlanetId = planet.PlanetId;
            pp.PlayerId = player.PlayerId;
            using (var repo = new PlayerPlanetsRepository())
            {
                var list = new List<PlayerPlanets>();
                list.Add(pp);
                repo.AddPlanetToPlayer(list);
            }

                #endregion
            // The stuff related to creating the first buildings a player starts with


                return player;
            
        }
    }
}
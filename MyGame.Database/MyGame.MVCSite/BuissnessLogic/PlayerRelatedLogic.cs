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
        /// The clean public version of creating a player, returns true or false depending non succeeding
        /// </summary>
        public bool CreateNewPlayer()
        {
            Players player = PrivateCreateNewPlayer();
            if (player != null)
                return true;
            return false;
        }
        /// <summary>
        /// Removing a player and everything he owns from the game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool RemovePlayer(Players player)
        {
            using (var repo = new PlayerRepository())
                if (repo.RemoveAPlayer(player))
                    return true;
            return false;
        }
        /// <summary>
        /// The messy private version of creating a player
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
using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class TroopsRelatedLogic
    {

        /// <summary>
        /// Checks if the player can afford,and has the required population to train the desired troops (Returns a bool on if it was successfull or not)
        /// </summary>
        public bool TrainNewTroopsForPlayer(Players player, List<PlayerTroops> playerTroops)
        {
            int counter = 0;
            int? minerals = 0;
            int? oil = 0;
            int? credits = 0;
            int? specialcredits = 0;
            int? specialresource = 0;
            int? totalQuantity = 0;


            // This loops through all the troops that the player wants to train and adds the entire cost of everything
            for (int i = 0; i < playerTroops.Count; i++)
            {
                minerals += playerTroops[i].Troops.Price.Minerals * playerTroops[i].Quantity;
                oil += playerTroops[i].Troops.Price.Oil * playerTroops[i].Quantity;
                credits += playerTroops[i].Troops.Price.Credits * playerTroops[i].Quantity;
                specialcredits += playerTroops[i].Troops.Price.SpecialCredits * playerTroops[i].Quantity;
                specialresource += playerTroops[i].Troops.Price.SpecialResource * playerTroops[i].Quantity;
                totalQuantity += playerTroops[i].Quantity * playerTroops[i].Troops.PeopleToOperate;

            }

            // This part loops through all the available resources for the player, and if the player has more of a given resource than he needs, counter gets one additional value
            using (var repo = new PlayerResourcesRepository())
            {
                PlayerResources pr = new PlayerResources();
                pr.PlayerId = player.PlayerId;
                var resources = repo.GetAllByPlayer(pr);
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].Resource.ResourceName == "Minerals" && resources[i].Quantity >= minerals)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Oil" && resources[i].Quantity >= oil)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Credits" && resources[i].Quantity >= credits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Credits" && resources[i].Quantity >= specialcredits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Resource" && resources[i].Quantity >= specialresource)
                        counter++;

                }
            }
            // This part adds all population that is above 50% of the max population of that planet to available for troop training.
            // If that number is higher than than quantity of troops requested counter is raised by one
            using (var repo = new PlayerPlanetsRepository())
            {
                int? totalPop = 0;
                PlayerPlanets pp = new PlayerPlanets();
                pp.PlayerId = player.PlayerId;
                var planets = repo.GetAllByPlayerAndPlanet(pp);
                for (int i = 0; i < planets.Count; i++)
                {
                    if (planets[i].Planet.CurrentPopulation > (planets[i].Planet.MaxPopulation / 2))
                        totalPop += planets[i].Planet.CurrentPopulation - (planets[i].Planet.MaxPopulation / 2);
                }
                // This part sorts all the planets according to PlanetNumber (which will be 1-2-3), it starts on the third planet trying to 
                // get enough population there to cover the troop training, if that is not possible, it takes as many as there are
                // and continues to the next planet. It will continue this way until all the troops are covered by population, alter the plents population
                // and update the database accordingly
                planets = planets.OrderBy(o => o.Planet.PlanetNumber).ToList();
                if (totalPop >= totalQuantity)
                {
                    for (int i = planets.Count; i > 0; i--)
                    {
                        if (planets[i].Planet.CurrentPopulation > (planets[i].Planet.MaxPopulation / 2))
                        {
                            if ((planets[i].Planet.CurrentPopulation - (planets[i].Planet.MaxPopulation / 2)) > totalQuantity)
                            {
                                planets[i].Planet.CurrentPopulation = -totalQuantity;
                                counter++;
                            }
                            else
                            {
                                planets[i].Planet.CurrentPopulation -= (planets[i].Planet.CurrentPopulation - (planets[i].Planet.MaxPopulation / 2));
                                totalQuantity -= (planets[i].Planet.CurrentPopulation - (planets[i].Planet.MaxPopulation / 2));
                            }
                        }
                    }
                }
                counter++;
                repo.AddOrUpdate(planets);
            }

            // If the counter has 6 here, it means that the player has more than he needs to pay for the troops and that there is available population to be drafted
            if (counter == 6)
            {
                using (var repo = new PlayerTroopsRepository())
                {
                    repo.AddOrUpdate(playerTroops);
                    counter++;
                }
            }
            if (counter == 7)
                return true;
            return false;
        }
        /// <summary>
        /// Removes troops from the player
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool RemovingTroopsFromPlayer(List<PlayerTroops> playerTroops)
        {
            using (var repo = new PlayerTroopsRepository())
            {

                if (repo.RemoveOrUpdate(playerTroops))
                    return true;
                return false;
            }
        }
    }
}
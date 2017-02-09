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
        /// <summary>
        /// Returns a list of all buildings a new player should start with
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public List<PlayerBuildings> GeneratePlayerBuildingsForNewPlayer(Players player)
        {
            List<PlayerBuildings> pb = new List<PlayerBuildings>();
            using (var repo = new BuildingsRepository())
            {
                var list = repo.GetAll();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].BuildingName == "Residential")
                    {
                        PlayerBuildings playerbuilding = new PlayerBuildings();
                        playerbuilding.BuildingId = list[i].BuildingId;
                        playerbuilding.PlayerId = player.PlayerId;
                        playerbuilding.Quantity = 50;
                        pb.Add(playerbuilding);
                    }
                    else if
                         (list[i].BuildingName == "Drills")
                    {
                        PlayerBuildings playerbuilding = new PlayerBuildings();
                        playerbuilding.BuildingId = list[i].BuildingId;
                        playerbuilding.PlayerId = player.PlayerId;
                        playerbuilding.Quantity = 50;
                        pb.Add(playerbuilding);
                    }
                    else if (list[i].BuildingName == "Mines")
                    {
                        PlayerBuildings playerbuilding = new PlayerBuildings();
                        playerbuilding.BuildingId = list[i].BuildingId;
                        playerbuilding.PlayerId = player.PlayerId;
                        playerbuilding.Quantity = 50;
                        pb.Add(playerbuilding);
                    }
                    else if (list[i].BuildingName == "Banks")
                    {
                        PlayerBuildings playerbuilding = new PlayerBuildings();
                        playerbuilding.BuildingId = list[i].BuildingId;
                        playerbuilding.PlayerId = player.PlayerId;
                        playerbuilding.Quantity = 50;
                        pb.Add(playerbuilding);
                    }
                }
            }
            return pb;
        }
        /// <summary>
        /// Checks if the player can afford,and has the required land, to build the desired buildings, and if he can constructs them (Returns a bool on if it was successfull or not)
        /// </summary>
        public bool ConstructBuildingsForPlayer(Players player, List<PlayerBuildings> playerbuildings)
        {
            int counter = 0;
            int? minerals = 0;
            int? oil = 0;
            int? credits = 0;
            int? specialcredits = 0;
            int? specialresource = 0;
            int totalquantity = 0;

            // This loops through all the buildings that the player wants to build and cadds the entire cost of everything
            for (int i = 0; i < playerbuildings.Count; i++)
            {
                minerals += playerbuildings[i].Building.Price.Minerals * playerbuildings[i].Quantity;
                oil += playerbuildings[i].Building.Price.Oil * playerbuildings[i].Quantity;
                credits += playerbuildings[i].Building.Price.Credits * playerbuildings[i].Quantity;
                specialcredits += playerbuildings[i].Building.Price.SpecialCredits * playerbuildings[i].Quantity;
                specialresource += playerbuildings[i].Building.Price.SpecialResource * playerbuildings[i].Quantity;
                totalquantity += playerbuildings[i].Quantity;

            }
            PlayerResources pr = new PlayerResources();
            pr.PlayerId = player.PlayerId;
            using (var repo = new PlayerResourcesRepository())
            {
                var resources = repo.GetAllByPlayer(pr);
                // This loops through all the available resources for the player, and if the player has more of a given resource than he needs, counter gets one additional value
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].Resource.ResourceName == "Minerals" && resources[i].Quantity > minerals)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Oil" && resources[i].Quantity > oil)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Credits" && resources[i].Quantity > credits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Credits" && resources[i].Quantity > specialcredits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Resource" && resources[i].Quantity > specialresource)
                        counter++;

                }
            }
            using (var repo = new PlayerPlanetsRepository())
            {
                PlayerPlanets pp = new PlayerPlanets();
                pp.Player.PlayerId = player.PlayerId;
                var list = repo.GetAllByPlayerAndPlanet(pp);
                for (int i = 0; i < list.Count; i++)
                {
                    if (playerbuildings[i].Planet.Free > totalquantity)
                    {
                        if (list[i].PlanetId == playerbuildings[0].PlanetId)
                        {
                            using (var repo2 = new PlanetsRepository())
                                repo2.Update(list[i].Planet);
                            counter++;

                        }
                    }

                }
            }
            // If the counter has 6 here, it means that the player has more than he needs to pay for the buildings and that there was enough free space on his planet to build the buildings
            if (counter == 6)
            {
                using (var repo = new PlayerBuildingsRepository())
                {
                    repo.AddOrUpdate(playerbuildings);
                    counter++;
                }


            }
            if (counter == 7)
                return true;
            return false;
        }
        public bool RazingBuildingsForPLayer(List<PlayerBuildings> playerBuildings)
        {
            using (var repo = new PlayerBuildingsRepository())
            {
                if (repo.RemoveOrUpdate(playerBuildings))
                    return true;
                return false;
                
            }
        }
    }
}

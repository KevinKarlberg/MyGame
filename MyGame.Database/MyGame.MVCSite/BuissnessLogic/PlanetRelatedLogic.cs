using MyGame.Database.Repositories;
using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class PlanetRelatedLogic
    {
        /// <summary>
        /// Generates a planet for a new player, these will always get "Starter" terrain type planets
        /// </summary>
        /// <returns></returns>
        public Planets GeneratePlanetForNewPlayer()
        {
            Planets planet = new Planets();
            planet.PlanetName = GeneratePlanetName();
            planet.PlanetId = Guid.NewGuid();
            using (var repo = new TerrainRepository())
            {
                planet.TerrainRefId = repo.GetGuidByTerrainType("Starter");
            }
            planet.CurrentSize = 3500;
            planet.MaxSize = 3500;
            planet.Free = 300;
            planet.Occupied = 200;

            return planet;

        }
        /// <summary>
        /// Generates a non starterplanet. Should implement logic later for rarity of good planets
        /// </summary>
        /// <returns></returns>
        public Planets GenerateNonStarterPlanet()
        {
            Random rnd = new Random();
            Planets planet = new Planets();
            planet.PlanetName = GeneratePlanetName();
            planet.PlanetId = Guid.NewGuid();
            using (var repo = new TerrainRepository())
            {
                var terrainlist = repo.GetAll();
                while (planet.Terrain != null && planet.Terrain.TerrainType != "Starter")
                {
                    int t = rnd.Next(0, terrainlist.Count);
                    planet.TerrainRefId = terrainlist[t].TerrainId;
                    planet.Terrain.TerrainType = terrainlist[t].TerrainType;

                }

            }
            int maxsize = rnd.Next(2000, 5001);
            int currentsize = 500 + (rnd.Next(0, 11) * 50);
            planet.MaxSize = maxsize;
            planet.CurrentSize = currentsize;
            planet.Free = currentsize;
            planet.Occupied = 0;
            return planet;
        }
        /// <summary>
        /// Generates a (hopefully) unique planetname
        /// </summary>
        /// <returns></returns>
        private string GeneratePlanetName()
        {
            string[] prefix = new string[] { "Proxima", "Habilita", "Oxima", "Glaxico", "Gaspra", "Dia", "Nida", "Apoxime", "Harambe", "Donaldia" };
            string[] suffix = new string[] { "Centaury", "Neo", "Supra", "Noxius", "Trumperio", "Diona", "Lingeria", "Sandrina", "Aluma", "Tindra", "Sparta" };
            Random rnd = new Random();
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            string planetName = $"{prefix[rnd.Next(0, prefix.Length)]} {suffix[rnd.Next(0, suffix.Length)]} {rnd.Next(1, 1000)}{alphabet[rnd.Next(0, alphabet.Length)].ToString()}";
            return planetName;


        }
    }
}
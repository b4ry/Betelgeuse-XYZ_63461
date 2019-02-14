using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class RegionModel
    {
        public string Name { get; set; }
        public SizeEnum Size { get; set; }
        public List<BiomeModel> Biomes { get; set; }
        public List<GameObject> NeighbourRegions { get; set; }
        public bool Visited { get; set; }

        public RegionModel(string name, SizeEnum size, BiomeEnum biomes, string[] biomeRarities, List<GameObject> neighbourRegions)
        {
            Name = name;
            Size = size;
            NeighbourRegions = neighbourRegions;
            Visited = false;

            Biomes = new List<BiomeModel>();

            var biomesArray = biomes.ToString().Replace(" ", "").Split(',');

            for (int i = 0; i < biomesArray.Length; i++)
            {
                var biomeEnum = (BiomeEnum)Enum.Parse(typeof(BiomeEnum), biomesArray[i]);
                var biomeRarityEnum = (RarityEnum)Enum.Parse(typeof(RarityEnum), biomeRarities[i]);

                Biomes.Add(new BiomeModel(biomeEnum, biomeRarityEnum));
            }
        }
    }
}

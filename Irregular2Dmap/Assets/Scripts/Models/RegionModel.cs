using Assets.Scripts.Controllers;
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
        public OddityModel Oddity { get; set; }

        public RegionModel(string name, SizeEnum size, BiomeEnum biomes, string[] biomeRarities, List<GameObject> neighbourRegions)
        {
            Name = name;
            Size = size;
            NeighbourRegions = neighbourRegions;
            Visited = false;

            RandomizeOddityRating();
            CreateBiomes(biomes, biomeRarities);
        }

        private void CreateBiomes(BiomeEnum biomes, string[] biomeRarities)
        {
            Biomes = new List<BiomeModel>();

            var biomesArray = biomes.ToString().Replace(" ", "").Split(',');

            for (int i = 0; i < biomesArray.Length; i++)
            {
                var biomeEnum = (BiomeEnum)Enum.Parse(typeof(BiomeEnum), biomesArray[i]);
                var biomeRarityEnum = (RarityEnum)Enum.Parse(typeof(RarityEnum), biomeRarities[i]);

                Biomes.Add(new BiomeModel(biomeEnum, biomeRarityEnum));
            }
        }

        private void RandomizeOddityRating()
        {
            Oddity = new OddityModel();

            var randomizedOddityValue = GameController.Instance.RNG.Next(0, 101);

            Debug.Log(randomizedOddityValue);

            Oddity.Rating = (float)(randomizedOddityValue <= 25 ? 0.5 :
                randomizedOddityValue <= 70 ? 1 :
                randomizedOddityValue <= 89 ? 1.5 :
                randomizedOddityValue <= 99 ? 2 : 4);

            if(Oddity.Rating == 0.5)
            {
                Oddity.Name = "BrokenOddity";
            }
            else if(Oddity.Rating == 1)
            {
                Oddity.Name = "NormalOddity";
            }
            else if (Oddity.Rating == 1.5)
            {
                Oddity.Name = "BiggerOddity";
            }
            else if (Oddity.Rating == 2)
            {
                Oddity.Name = "SparklingOddity";
            }
            else if (Oddity.Rating == 4)
            {
                Oddity.Name = "PerfectOddity";
            }
        }
    }
}

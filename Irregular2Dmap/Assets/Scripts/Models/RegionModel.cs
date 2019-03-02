using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class RegionModel
    {
        private readonly Dictionary<RarityEnum, int> rarityOccurenceThresholds = new Dictionary<RarityEnum, int>()
        {
            { RarityEnum.Common, 0 },
            { RarityEnum.Uncommon, 20 },
            { RarityEnum.Rare, 40 },
            { RarityEnum.Legendary, 60 }
        };

        public string Name { get; set; }
        public RegionSizeEnum Size { get; set; }
        public List<BiomeModel> Biomes { get; set; }
        public List<ResourceModel> Resources { get; set; }
        public List<GameObject> NeighbourRegions { get; set; }
        public bool Visited { get; set; }
        public OddityModel Oddity { get; set; }

        public RegionModel(string name, RegionSizeEnum size, string[] biomeNames, List<GameObject> neighbourRegions)
        {
            Name = name;
            Size = size;
            NeighbourRegions = neighbourRegions;
            Visited = false;

            RandomizeOddityRating();
            CreateBiomes(biomeNames);
            CalculateResourcesAvailability();
        }

        private void CreateBiomes(string[] biomeNames)
        {
            Biomes = new List<BiomeModel>();

            foreach(var biomeName in biomeNames)
            {
                var biomeModel = DefinitionsController.Instance.BiomeDefinitions[biomeName];
                var newBiomeModel = new BiomeModel(biomeModel.Name, biomeModel.Rarity, biomeModel.Resources);

                Biomes.Add(newBiomeModel);
            }

            DistributeAreaAmongBiomes();
        }

        private void CalculateResourcesAvailability()
        {
            Resources = new List<ResourceModel>();

            foreach (var biomeModel in Biomes)
            {
                foreach (var resourceModel in biomeModel.Resources)
                {
                    var isResourceAvailable = IsResourceAvailable(biomeModel, resourceModel);
                    resourceModel.DetermineDeposits(this, biomeModel);

                    var isResourceAlreadyAvailalbe = Resources.Any(rtd => rtd.Name == resourceModel.Name);

                    if (isResourceAvailable && !isResourceAlreadyAvailalbe)
                    {
                        var newResourceModel = new ResourceModel(resourceModel.Name, resourceModel.Rarity, resourceModel.DepositType, resourceModel.DepositAmount);

                        Resources.Add(newResourceModel);
                    }
                    else if(isResourceAlreadyAvailalbe)
                    {
                        Debug.Log($"Merging deposits for: {resourceModel.Name}; Adding {resourceModel.DepositAmount}");

                        var resourceToMerge = Resources.FirstOrDefault(res => res.Name == resourceModel.Name);

                        Debug.Log($"Before: {resourceToMerge.DepositAmount}");

                        resourceToMerge.MergeDeposits(resourceModel);

                        Debug.Log($"After: {resourceToMerge.DepositAmount}");
                    }
                }
            }
        }

        private bool IsResourceAvailable(BiomeModel biomeModel, ResourceModel resourceModel)
        {
            var occurenceRating = biomeModel.Area * 2 * (int)biomeModel.Rarity * (GameController.Instance.RNG.NextDouble() + 0.2) * Oddity.Rating / (int)resourceModel.Rarity;

            return occurenceRating >= rarityOccurenceThresholds[resourceModel.Rarity];
        }

        private void DistributeAreaAmongBiomes()
        {
            int pool = 0;
            int maxSize = (int)Size;
            int biomeMaxSize = maxSize / Biomes.Count;

            foreach (var biome in Biomes)
            {
                biome.Area = GameController.Instance.RNG.Next(1, biomeMaxSize + 1);
                pool += biomeMaxSize - biome.Area;
            }

            int leftovers = pool / Biomes.Count;
            int totalBiomesSize = 0;

            foreach (var biome in Biomes)
            {
                biome.Area += leftovers;
                totalBiomesSize += biome.Area;
            }

            int roundLeftover = maxSize - totalBiomesSize;

            Biomes[GameController.Instance.RNG.Next(0, Biomes.Count)].Area += roundLeftover;

            foreach(var biome in Biomes)
            {
                biome.AreaPercentage = (float)biome.Area / (int)Size * 100;
            }
        }

        private void RandomizeOddityRating()
        {
            Oddity = new OddityModel();

            var randomizedOddityValue = GameController.Instance.RNG.Next(0, 101);

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

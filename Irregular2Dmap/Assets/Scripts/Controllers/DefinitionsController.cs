using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class DefinitionsController : MonoBehaviour
    {
        public static DefinitionsController Instance = null;

        public Dictionary<string, BiomeModel> BiomeDefinitions = new Dictionary<string, BiomeModel>();
        public Dictionary<string, ResourceModel> ResourceDefinitions = new Dictionary<string, ResourceModel>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            ReadResourceDefinitions();
            ReadBiomeDefinitions();

            DontDestroyOnLoad(gameObject);
        }

        private void ReadResourceDefinitions()
        {
            var path = $"/Assets/Definitions/ResourcesDefinition/AvailableResources.txt";
            string[] availableResources = FileReader.ReadFile(path);

            foreach (var availableResource in availableResources)
            {
                path = $"/Assets/Definitions/ResourcesDefinition/{availableResource}.txt";

                string[] resourceDefinition = FileReader.ReadFile(path);

                Enum.TryParse(resourceDefinition[1], out RarityEnum resourceRarity);

                var newResourceModel = new ResourceModel(resourceDefinition[0], resourceRarity);

                ResourceDefinitions.Add(resourceDefinition[0], newResourceModel);
            }
        }

        private void ReadBiomeDefinitions()
        {
            var path = $"/Assets/Definitions/BiomesDefinition/AvailableBiomes.txt";
            string[] availableBiomes = FileReader.ReadFile(path);

            foreach (var availableBiome in availableBiomes)
            {
                path = $"/Assets/Definitions/BiomesDefinition/{availableBiome}.txt";

                string[] biomeDefinition = FileReader.ReadFile(path);

                Enum.TryParse(biomeDefinition[1], out RarityEnum biomeRarity);

                var biomeResourceModels = new List<ResourceModel>();
                var resources = biomeDefinition[2].Split(';');
                
                foreach(var resource in resources)
                {
                    var resourceDefinition = ResourceDefinitions[resource];
                    var newResourceModel = new ResourceModel(resourceDefinition.Name, resourceDefinition.RarityEnum);

                    biomeResourceModels.Add(newResourceModel);
                }

                var newBiomeModel = new BiomeModel(biomeDefinition[0], biomeRarity, biomeResourceModels);

                BiomeDefinitions.Add(biomeDefinition[0], newBiomeModel);
            }
        }
    }
}

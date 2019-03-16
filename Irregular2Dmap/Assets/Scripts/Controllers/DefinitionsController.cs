using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
using Assets.Scripts.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class DefinitionsController : MonoBehaviour
    {
        public static DefinitionsController Instance = null;

        public Dictionary<string, BiomeDefinitionModel> BiomeDefinitions = new Dictionary<string, BiomeDefinitionModel>();
        public Dictionary<string, ResourceDefinitionModel> ResourceDefinitions = new Dictionary<string, ResourceDefinitionModel>();
        public Dictionary<string, TileLayerDefinitionModel> TileLayerDefinitions = new Dictionary<string, TileLayerDefinitionModel>();

        void Awake()
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
            ReadTileLayerDefinitions();

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            ReadBuildingsDefinitions(GameController.Instance.PlayerManager.Race);
        }

        private void ReadBuildingsDefinitions(RaceEnum race)
        {
            var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Buildings/{race.ToString()}";
            var buildingDefinitionPaths = Directory.GetFiles(path, "*.txt");

            foreach(var buildingDefinitionPath in buildingDefinitionPaths)
            {
                string[] buildingDefinition = FileReader.ReadFile(buildingDefinitionPath, false);

                Enum.TryParse(buildingDefinition[1], out BuildingTypeEnum buildingType);
                int.TryParse(buildingDefinition[3], out int isAvailable);

                var costs = buildingDefinition[2].Split(';');
                var costModel = new CostModel
                {
                    Resources = new Dictionary<string, float>()
                };

                foreach (var cost in costs)
                {
                    var splitCost = cost.Split('-');

                    costModel.Resources.Add(splitCost[0], float.Parse(splitCost[1]));
                }

                var buildingModel = new BuildingModel(buildingDefinition[0], buildingType, costModel, Convert.ToBoolean(isAvailable));
            }
        }

        private void ReadResourceDefinitions()
        {
            var path = $"/Assets/Definitions/Resources/AvailableResources.txt";
            string[] availableResources = FileReader.ReadFile(path, true);

            foreach (var availableResource in availableResources)
            {
                path = $"/Assets/Definitions/Resources/{availableResource}.txt";

                string[] resourceDefinition = FileReader.ReadFile(path, true);

                Enum.TryParse(resourceDefinition[1], out RarityEnum resourceRarity);

                var newResourceDefinitionModel = new ResourceDefinitionModel(resourceDefinition[0], resourceRarity);

                ResourceDefinitions.Add(resourceDefinition[0], newResourceDefinitionModel);
            }
        }

        private void ReadBiomeDefinitions()
        {
            var path = $"/Assets/Definitions/Biomes/AvailableBiomes.txt";
            string[] availableBiomes = FileReader.ReadFile(path, true);

            foreach (var availableBiome in availableBiomes)
            {
                path = $"/Assets/Definitions/Biomes/{availableBiome}.txt";

                string[] biomeDefinition = FileReader.ReadFile(path, true);

                Enum.TryParse(biomeDefinition[1], out RarityEnum biomeRarity);

                var biomeResourceDefinitionModels = new List<ResourceDefinitionModel>();
                var resources = biomeDefinition[2].Split(';');
                
                foreach(var resource in resources)
                {
                    var resourceDefinition = ResourceDefinitions[resource];
                    var newResourceDefinitionModel = new ResourceDefinitionModel(resourceDefinition.Name, resourceDefinition.RarityEnum);

                    biomeResourceDefinitionModels.Add(newResourceDefinitionModel);
                }

                var newBiomeDefinitionModel = new BiomeDefinitionModel(biomeDefinition[0], biomeRarity, biomeResourceDefinitionModels);

                BiomeDefinitions.Add(biomeDefinition[0], newBiomeDefinitionModel);
            }
        }

        private void ReadTileLayerDefinitions()
        {
            var path = $"/Assets/Definitions/TileLayers/AvailableTileLayers.txt";
            string[] availableTileLayers = FileReader.ReadFile(path, true);

            foreach (var availableTileLayer in availableTileLayers)
            {
                path = $"/Assets/Definitions/TileLayers/{availableTileLayer}.txt";

                string[] tileLayerDefinition = FileReader.ReadFile(path, true);

                Enum.TryParse(tileLayerDefinition[1], out MaterialHardnessEnum materialHardness);

                var newTileLayerDefinitionModel = new TileLayerDefinitionModel(tileLayerDefinition[0], materialHardness);

                TileLayerDefinitions.Add(tileLayerDefinition[0], newTileLayerDefinitionModel);
            }
        }
    }
}

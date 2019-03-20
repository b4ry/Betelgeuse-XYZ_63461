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
        public Dictionary<string, BuildingDefinitionModel> BuildingDefinitions = new Dictionary<string, BuildingDefinitionModel>();

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
            ReadBuildingsDefinitions(GameController.Instance.Race);
            GameController.Instance.ProducePlayer();
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

                var buildingModel = new BuildingDefinitionModel(buildingDefinition[0], buildingType, costModel, Convert.ToBoolean(isAvailable));

                BuildingDefinitions.Add(buildingModel.Name, buildingModel);
            }
        }

        private void ReadResourceDefinitions()
        {
            var availableResourcesPath = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Resources/AvailableResources.xml";
            var availableResources = AvailableResourcesDefinitionModel.Load(availableResourcesPath);

            foreach (var availableResource in availableResources.Resources)
            {
                var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Resources/{availableResource.Name}.xml";
                var resourceDefinition = ResourceDefinitionModel.Load(path);
                var newResourceDefinitionModel = new ResourceDefinitionModel(resourceDefinition.Name, resourceDefinition.Rarity);

                ResourceDefinitions.Add(resourceDefinition.Name, newResourceDefinitionModel);
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
                    var newResourceDefinitionModel = new ResourceDefinitionModel(resourceDefinition.Name, resourceDefinition.Rarity);

                    biomeResourceDefinitionModels.Add(newResourceDefinitionModel);
                }

                var newBiomeDefinitionModel = new BiomeDefinitionModel(biomeDefinition[0], biomeRarity, biomeResourceDefinitionModels);

                BiomeDefinitions.Add(biomeDefinition[0], newBiomeDefinitionModel);
            }
        }

        private void ReadTileLayerDefinitions()
        {
            var availableTileLayersPath = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/TileLayers/AvailableTileLayers.xml";
            var availableTileLayers = AvailableTileLayersDefinitionModel.Load(availableTileLayersPath);

            foreach (var availableTileLayer in availableTileLayers.TileLayers)
            {
                var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/TileLayers/{availableTileLayer.Name}.xml";
                var tileLayerDefinition = TileLayerDefinitionModel.Load(path);
                var newTileLayerDefinitionModel = new TileLayerDefinitionModel(tileLayerDefinition.Name, tileLayerDefinition.MaterialHardness);

                TileLayerDefinitions.Add(tileLayerDefinition.Name, newTileLayerDefinitionModel);
            }
        }
    }
}

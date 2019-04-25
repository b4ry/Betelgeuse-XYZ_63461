using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
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
        public Dictionary<RaceEnum, Dictionary<string, BuildingDefinitionModel>> BuildingDefinitions = new Dictionary<RaceEnum, Dictionary<string, BuildingDefinitionModel>>();

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

        public void ReadBuildingsDefinitions(PlayerModel player)
        {
            if (!BuildingDefinitions.ContainsKey(player.Race))
            {
                var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Buildings/{player.Race.ToString()}";
                var buildingDefinitionPaths = Directory.GetFiles(path, "*.xml");

                BuildingDefinitions.Add(player.Race, new Dictionary<string, BuildingDefinitionModel>());

                foreach (var buildingDefinitionPath in buildingDefinitionPaths)
                {
                    var buildingDefinition = BuildingDefinitionModel.Load(buildingDefinitionPath);
                    var buildingModel = new BuildingDefinitionModel(buildingDefinition.Name, buildingDefinition.BuildingType, buildingDefinition.Cost, buildingDefinition.Available);

                    BuildingDefinitions[player.Race].Add(buildingModel.Name, buildingModel);
                }
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
            var availableBiomesPath = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Biomes/AvailableBiomes.xml";
            var availableBiomes = AvailableBiomesDefinitionModel.Load(availableBiomesPath);

            foreach (var availableBiome in availableBiomes.Biomes)
            {
                var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Biomes/{availableBiome.Name}.xml";
                var biomeDefinition = BiomeDefinitionModel.Load(path);              
                var biomeResourceDefinitionModels = new List<ResourceDefinitionModel>();
                
                foreach(var resource in biomeDefinition.ResourceDefinitions)
                {
                    var resourceDefinition = ResourceDefinitions[resource.Name];
                    var newResourceDefinitionModel = new ResourceDefinitionModel(resourceDefinition.Name, resourceDefinition.Rarity);

                    biomeResourceDefinitionModels.Add(newResourceDefinitionModel);
                }

                var newBiomeDefinitionModel = new BiomeDefinitionModel(biomeDefinition.Name, biomeDefinition.Rarity, biomeResourceDefinitionModels);

                BiomeDefinitions.Add(biomeDefinition.Name, newBiomeDefinitionModel);
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

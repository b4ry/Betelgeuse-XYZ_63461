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

            var path = $"/Assets/Definitions/BiomesDefinition/AvailableBiomes.txt";
            string[] availableBiomes = FileReader.ReadFile(path);

            foreach(var availableBiome in availableBiomes)
            {
                path = $"/Assets/Definitions/BiomesDefinition/{availableBiome}.txt";

                string[] biomeDefinition = FileReader.ReadFile(path);

                Enum.TryParse(biomeDefinition[1], out RarityEnum biomeRarity);

                var newBiomeModel = new BiomeModel(biomeDefinition[0], biomeRarity);

                BiomeDefinitions.Add(biomeDefinition[0], newBiomeModel);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}

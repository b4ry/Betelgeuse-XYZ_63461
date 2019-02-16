using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BiomeDefinitionsController : MonoBehaviour
    {
        public static BiomeDefinitionsController Instance = null;

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

            var path = $"/Assets/BiomesDefinition/AvailableBiomes.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] availableBiomes = File.ReadAllLines(fullPath);

            foreach(var availableBiome in availableBiomes)
            {
                path = $"/Assets/BiomesDefinition/{availableBiome}.txt";
                fullPath = string.Concat(directory, path);

                string[] biomeDefinition = File.ReadAllLines(fullPath);

                Enum.TryParse(biomeDefinition[1], out RarityEnum biomeRarity);

                var newBiomeModel = new BiomeModel(biomeDefinition[0], biomeRarity);

                BiomeDefinitions.Add(biomeDefinition[0], newBiomeModel);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}

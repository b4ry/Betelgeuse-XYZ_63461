using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class RegionModel
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public Biome Biomes { get; set; }
        public List<GameObject> NeighbourRegions { get; set; }
        public bool Visited { get; set; }

        public RegionModel(string name, Size size, Biome biomes, List<GameObject> neighbourRegions)
        {
            Name = name;
            Size = size;
            Biomes = biomes;
            NeighbourRegions = neighbourRegions;
            Visited = false;
        }
    }
}

using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class BiomeModel
    {
        //public BiomeEnum Biome { get; set; }

        public int Area { get; set; }
        public string Name { get; set; }

        public RarityEnum Rarity { get; set; }
        public List<ResourceModel> Resources { get; set; }

        public BiomeModel(string name, RarityEnum rarity, List<ResourceModel> resources)
        {
            Name = name;
            Rarity = rarity;
            Area = 0;
            Resources = resources;
        }
    }
}

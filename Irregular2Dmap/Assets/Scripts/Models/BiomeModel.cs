using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class BiomeModel : IFoundableObjectModel
    {
        //public BiomeEnum Biome { get; set; }

        public int Area { get; set; }

        public float AreaPercentage { get; set; }
        public string Name { get; set; }

        public RarityEnum Rarity { get; set; }

        public List<ResourceModel> Resources { get; set; }
        public List<List<ExplorationGameTileModel>> Tiles { get; set; }

        public BiomeModel(string name, RarityEnum rarity, List<ResourceDefinitionModel> resources)
        {
            Name = name;
            Rarity = rarity;
            Area = 0;
            AreaPercentage = 0;

            Resources = new List<ResourceModel>();

            foreach(var resource in resources)
            {
                var newResourceModel = new ResourceModel(resource.Name, resource.RarityEnum);

                Resources.Add(newResourceModel);
            }
        }
    }
}

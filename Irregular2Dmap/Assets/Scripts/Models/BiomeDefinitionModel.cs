using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class BiomeDefinitionModel
    {
        public string Name;
        public RarityEnum RarityEnum;
        public List<ResourceDefinitionModel> ResourceDefinitions;

        public BiomeDefinitionModel(string name, RarityEnum rarityEnum, List<ResourceDefinitionModel> resourceDefinitions)
        {
            Name = name;
            RarityEnum = rarityEnum;
            ResourceDefinitions = resourceDefinitions;
        }
    }
}

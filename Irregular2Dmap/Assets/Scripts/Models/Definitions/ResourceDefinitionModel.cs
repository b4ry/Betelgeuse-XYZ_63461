using Assets.Scripts.Enums;

namespace Assets.Scripts.Models.Definitions
{
    public class ResourceDefinitionModel
    {
        public string Name;
        public RarityEnum RarityEnum;

        public ResourceDefinitionModel(string name, RarityEnum rarityEnum)
        {
            Name = name;
            RarityEnum = rarityEnum;
        }
    }
}

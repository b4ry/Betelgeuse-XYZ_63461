using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class ResourceModel
    {
        public string Name { get; set; }
        public RarityEnum RarityEnum { get; set; }

        public ResourceModel(string name, RarityEnum rarityEnum)
        {
            Name = name;
            RarityEnum = rarityEnum;
        }
    }
}

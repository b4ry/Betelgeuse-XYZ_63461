using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class ResourceModel : IModel
    {
        public string Name { get; set; }
        public RarityEnum Rarity { get; set; }

        public ResourceModel(string name, RarityEnum rarityEnum)
        {
            Name = name;
            Rarity = rarityEnum;
        }

        public override bool Equals(object obj)
        {
            var resourceModel = obj as ResourceModel;

            return Equals(resourceModel);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Rarity.GetHashCode();
        }

        private bool Equals(ResourceModel resourceModel)
        {
            return Name.Equals(resourceModel.Name);
        }
    }
}

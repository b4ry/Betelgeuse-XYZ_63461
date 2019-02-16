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

        public override bool Equals(object obj)
        {
            var resourceModel = obj as ResourceModel;

            return Equals(resourceModel);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ RarityEnum.GetHashCode();
        }

        private bool Equals(ResourceModel resourceModel)
        {
            return Name.Equals(resourceModel.Name);
        }
    }
}

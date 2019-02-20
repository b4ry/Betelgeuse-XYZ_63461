using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public interface IModel
    {
        string Name { get; set; }
        RarityEnum Rarity { get; set; }
    }
}

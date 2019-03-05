using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public interface IFoundableObjectModel
    {
        string Name { get; set; }
        RarityEnum Rarity { get; set; }
    }
}

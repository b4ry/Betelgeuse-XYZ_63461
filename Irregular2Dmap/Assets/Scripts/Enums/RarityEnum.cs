using System.Xml.Serialization;

namespace Assets.Scripts.Enums
{
    public enum RarityEnum
    {
        [XmlEnum("Common")]
        Common = 1,
        [XmlEnum("Uncommon")]
        Uncommon = 2,
        [XmlEnum("Rare")]
        Rare = 3,
        [XmlEnum("Legendary")]
        Legendary = 4
    }
}

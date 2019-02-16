using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class BiomeModel
    {
        //public BiomeEnum Biome { get; set; }
        public string Name { get; set; }
        public RarityEnum Rarity { get; set; }
        public int Area { get; set; }

        public BiomeModel(string name, RarityEnum rarity)
        {
            Name = name;
            Rarity = rarity;
            Area = 0;
        }
    }
}

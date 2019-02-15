using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class BiomeModel
    {
        public BiomeEnum Biome { get; set; }
        public RarityEnum Rarity { get; set; }
        public int Area { get; set; }

        public BiomeModel(BiomeEnum biome, RarityEnum rarity)
        {
            Biome = biome;
            Rarity = rarity;
            Area = 0;
        }
    }
}

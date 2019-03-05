using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class ExplorationGameTileModel
    {
        public TileLayerTypeEnum TileLayerType;
        public ThicknessEnum Thickness;
        public ProcessingDifficultyEnum ProcessingDifficulty;
        public List<IFoundableObjectModel> Items;
    }
}

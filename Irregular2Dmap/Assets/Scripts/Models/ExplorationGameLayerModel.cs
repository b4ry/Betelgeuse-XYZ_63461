using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public struct ExplorationGameLayerModel
    {
        public TileLayerModel TileLayerModel { get; set; }
        public List<IFoundableObjectModel> Items { get; set; }
        public Sprite LayerSprite { get; set; }

        public ExplorationGameLayerModel(TileLayerModel tileLayerModel, Sprite layerSprite)
        {
            TileLayerModel = tileLayerModel;
            Items = new List<IFoundableObjectModel>();
            LayerSprite = layerSprite;
        }
    }
}

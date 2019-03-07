using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationGameTileController : MonoBehaviour
    {
        private List<ExplorationGameLayerModel> layers;
        private SpritesReader spritesReader;

        public void SetupTile(List<ExplorationGameLayerModel> tileLayers, SpritesReader spritesReader)
        {
            layers = tileLayers;
            this.spritesReader = spritesReader;

            gameObject.GetComponent<Image>().sprite =
                this.spritesReader.LayersImageSprites
                    .OrderBy(x => GameController.Instance.RNG.Next())
                    .FirstOrDefault(s => s.name.Contains(layers.FirstOrDefault().TileLayerModel.Name));
        }

        public void OnMouseDown()
        {
            Debug.Log("---------------------------------------------------------------------");
            Debug.Log("Layer type: " + layers.FirstOrDefault().TileLayerModel.Name);
            Debug.Log("Layers number: " + layers.Count);
        }
    }
}

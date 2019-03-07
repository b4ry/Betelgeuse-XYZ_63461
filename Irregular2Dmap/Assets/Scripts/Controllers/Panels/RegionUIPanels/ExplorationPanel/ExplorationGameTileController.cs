using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationGameTileController : MonoBehaviour
    {
        private List<ExplorationGameLayerModel> layers;
        private GameObject selectImage;

        public void SetupTile(List<ExplorationGameLayerModel> tileLayers, GameObject select)
        {
            layers = tileLayers;
            selectImage = select;

            gameObject.GetComponent<Image>().sprite = layers.FirstOrDefault().LayerSprite;
        }

        public void OnMouseDown()
        {
            Debug.Log("---------------------------------------------------------------------");
            Debug.Log("Layer type: " + layers.FirstOrDefault().TileLayerModel.Name);
            Debug.Log("Layers number: " + layers.Count);

            DetermineSelection();
        }

        private void DetermineSelection()
        {
            var selectXPosition = selectImage.transform.localPosition.x;
            var selectYPosition = selectImage.transform.localPosition.y;

            var newSelectXPosition = gameObject.transform.localPosition.x;
            var newSelectYPosition = gameObject.transform.localPosition.y;

            if (!(selectXPosition != newSelectXPosition || selectYPosition != newSelectYPosition))
            {
                selectImage.SetActive(!selectImage.activeSelf);
            }

            selectImage.transform.localPosition = gameObject.transform.localPosition;
        }
    }
}

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
        private int index;
        private GameObject dig;

        public void SetupTile(List<ExplorationGameLayerModel> tileLayers, GameObject select, int tileNumber, GameObject dig)
        {
            layers = tileLayers;
            selectImage = select;
            index = tileNumber;
            this.dig = dig;

            gameObject.GetComponent<Image>().sprite = layers.FirstOrDefault().LayerSprite;
        }

        public void OnMouseDown()
        {
            Debug.Log("---------------------------------------------------------------------");
            Debug.Log("Index: " + index);
            Debug.Log("Layer type: " + layers.FirstOrDefault().TileLayerModel.Name);
            Debug.Log("Layers number: " + layers.Count);

            gameObject.name += index;

            DetermineSelection();
        }

        private void DetermineSelection()
        {
            var selectXPosition = selectImage.transform.localPosition.x;
            var selectYPosition = selectImage.transform.localPosition.y;

            var newSelectXPosition = gameObject.transform.localPosition.x;
            var newSelectYPosition = gameObject.transform.localPosition.y;

            var button = dig.GetComponent<Button>();

            if (selectXPosition != newSelectXPosition || selectYPosition != newSelectYPosition)
            {
                selectImage.SetActive(true);
                button.interactable = true;
            }
            else if(selectXPosition == newSelectXPosition && selectYPosition == newSelectYPosition)
            {
                var isActive = selectImage.activeSelf;

                if (!isActive)
                {
                    selectImage.SetActive(true);
                    button.interactable = true;
                }
                else
                {
                    selectImage.SetActive(false);
                    button.interactable = false;
                }
            }

            selectImage.transform.localPosition = gameObject.transform.localPosition;
        }
    }
}

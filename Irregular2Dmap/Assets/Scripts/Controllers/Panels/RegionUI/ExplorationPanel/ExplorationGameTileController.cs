﻿using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUI.ExplorationPanel
{
    public class ExplorationGameTileController : MonoBehaviour
    {
        private List<ExplorationGameLayerModel> layers;
        private GameObject selectImage;
        private GameObject dig;
        private Image image;
        private Sprite holeSprite;

        public void SetupTile(List<ExplorationGameLayerModel> tileLayers, GameObject select, GameObject dig, Sprite holeSprite)
        {
            layers = tileLayers;
            selectImage = select;
            this.dig = dig;
            this.holeSprite = holeSprite;

            image = gameObject.GetComponent<Image>();

            if (layers.Count > 0)
            {
                image.sprite = layers.FirstOrDefault().LayerSprite;
            }
            else
            {
                image.sprite = holeSprite;
            }
            
        }

        public void OnMouseDown()
        {
            var button = dig.GetComponent<Button>();

            DetermineSelection();

            if (selectImage.activeSelf && layers.Count > 0)
            {
                SetupDigButton(button, true);
            }
            else
            {
                SetupDigButton(button, false);
            }
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
            }
            else if(selectXPosition == newSelectXPosition && selectYPosition == newSelectYPosition)
            {
                var isActive = selectImage.activeSelf;

                if (!isActive)
                {
                    selectImage.SetActive(true);
                }
                else
                {
                    selectImage.SetActive(false);
                }
            }

            selectImage.transform.localPosition = gameObject.transform.localPosition;
        }

        private void SetupDigButton(Button button, bool isInteractable)
        {
            button.interactable = isInteractable;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate
            {
                DigTile();
            });
        }

        private void DigTile()
        {
            Debug.Log("DIGGING");

            if (layers.Count > 0)
            {
                layers.RemoveAt(0);
            }
            else
            {
                Debug.Log("THERE IS NOTHING LEFT, SIRE!");
            }

            if(layers.Count > 0)
            {
                image.sprite = layers.FirstOrDefault().LayerSprite;
            }
            else
            {
                dig.GetComponent<Button>().interactable = false;
                image.sprite = holeSprite;
            }
        }
    }
}

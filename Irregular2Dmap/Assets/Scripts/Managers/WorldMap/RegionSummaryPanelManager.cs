﻿using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.WorldMap
{
    public class RegionSummaryPanelManager : MonoBehaviour
    {
        public static RegionSummaryPanelManager Instance = null;

        private const int MaxNumberOfObjectsToDisplay = 6;
        private const int ImageOffset = 20;
        private const int UnknownImageOffset = 55;
        private const int RarityDisplayAllXOffset = 18;
        private const int ImageDisplayAllXOffset = 10;
        private const int RarityXOffset = 53;
        private const int ImageXOffset = 55;

        private const string UnknownString = "Unknown";
        private const string UnchartedLandString = "Uncharted Land";
        private const string SizeString = "Size";
        private const string BiomesString = "Biomes";
        private const string ResourcesString = "Resources";
        private const string ColonDelimiterString = ": ";

        [SerializeField]
        private GameObject sprites;
        [SerializeField]
        private GameObject regionSummaryPanel;

        [SerializeField]
        private GameObject regionName;
        [SerializeField]
        private GameObject size;
        [SerializeField]
        private GameObject biomes;
        [SerializeField]
        private GameObject resources;

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI sizeTextMesh;
        private TextMeshProUGUI biomesTextMesh;
        private TextMeshProUGUI resourcesTextMesh;

        [SerializeField]
        private Button chartButton;
        [SerializeField]
        private Button enterButton;
        [SerializeField]
        private Button moreBiomesButton;
        [SerializeField]
        private Button moreResourcesButton;

        [SerializeField]
        private GameObject biomeTooltip;
        [SerializeField]
        private GameObject resourceTooltip;
        [SerializeField]
        private GameObject allBiomesTooltip;
        [SerializeField]
        private GameObject allResourcesTooltip;

        [SerializeField]
        private GameObject biomeImagePrefab;
        [SerializeField]
        private GameObject resourceImagePrefab;
        [SerializeField]
        private GameObject rarityImagePrefab;

        [SerializeField]
        private GameObject oddityImage;

        #region MoreImagesPanel

        [SerializeField]
        private GameObject allBiomes;
        [SerializeField]
        private GameObject allResources;

        [SerializeField]
        private GameObject allBiomesImagePrefab;
        [SerializeField]
        private GameObject allResourcesImagePrefab;

        [SerializeField]
        private GameObject allBiomesPanel;
        [SerializeField]
        private GameObject allResourcesPanel;

        #endregion

        private List<GameObject> biomeImageObjects = new List<GameObject>();
        private List<GameObject> resourceImageObjects = new List<GameObject>();
        private List<GameObject> rarityImageObjects = new List<GameObject>();

        private SpritesReader spritesReader;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            nameTextMesh = regionName.GetComponent<TextMeshProUGUI>();
            sizeTextMesh = size.GetComponent<TextMeshProUGUI>();
            biomesTextMesh = biomes.GetComponent<TextMeshProUGUI>();
            resourcesTextMesh = resources.GetComponent<TextMeshProUGUI>();

            spritesReader = sprites.GetComponent<SpritesReader>();
        }

        public void SetActive(bool active)
        {
            regionSummaryPanel.SetActive(active);
        }

        public void SetupRegionSummaryPanel(RegionModel regionModel, bool isLandUncharted)
        {
            var regionName = regionModel.Name;

            if (isLandUncharted)
            {
                regionName = UnchartedLandString;
            }

            nameTextMesh.SetText(regionName);
            sizeTextMesh.SetText(SizeString + ColonDelimiterString + regionModel.Size);
            biomesTextMesh.SetText(BiomesString + ColonDelimiterString);
            resourcesTextMesh.SetText(ResourcesString + ColonDelimiterString);

            ClearPanel();

            SetupImages(regionModel.Biomes, biomeImageObjects, isLandUncharted, spritesReader.BiomeImageSprites, biomes, biomeImagePrefab, false);

            if(regionModel.Biomes.Count > MaxNumberOfObjectsToDisplay && !isLandUncharted)
            {
                SetupImages(regionModel.Biomes, biomeImageObjects, isLandUncharted, spritesReader.BiomeImageSprites, allBiomes, allBiomesImagePrefab, true);
                moreBiomesButton.gameObject.SetActive(true);
            }
            else
            {
                moreBiomesButton.gameObject.SetActive(false);

                if (allBiomesPanel.activeSelf)
                {
                    allBiomesPanel.SetActive(false);
                }
            }

            SetupImages(regionModel.Resources, resourceImageObjects, isLandUncharted, spritesReader.ResourceImageSprites, resources, resourceImagePrefab, false);

            if (regionModel.Resources.Count > MaxNumberOfObjectsToDisplay && !isLandUncharted)
            {
                SetupImages(regionModel.Resources, resourceImageObjects, isLandUncharted, spritesReader.ResourceImageSprites, allResources, allResourcesImagePrefab, true);
                moreResourcesButton.gameObject.SetActive(true);
            }
            else
            {
                moreResourcesButton.gameObject.SetActive(false);

                if(allResourcesPanel.activeSelf)
                {
                    allResourcesPanel.SetActive(false);
                }
            }

            if (!isLandUncharted)
            {
                oddityImage.GetComponent<Image>().sprite = spritesReader.OddityImageSprites.FirstOrDefault(ois => ois.name.Equals(regionModel.Oddity.Name));
            }
            else
            {
                oddityImage.GetComponent<Image>().sprite = spritesReader.OddityImageSprites.FirstOrDefault(ois => ois.name.Equals(UnknownString));
            }

            chartButton.interactable = isLandUncharted;
            enterButton.interactable = !isLandUncharted;

            if (!regionSummaryPanel.activeSelf)
            {
                regionSummaryPanel.SetActive(true);
            }
        }

        public void SetupEnterButton(UnityAction action)
        {
            enterButton.onClick.RemoveAllListeners();
            enterButton.onClick.AddListener(action);
        }

        public void SetupChartButton(UnityAction action)
        {
            chartButton.onClick.RemoveAllListeners();
            chartButton.onClick.AddListener(action);
        }

        public void ShowBiomeTooltip(bool show)
        {
            biomeTooltip.SetActive(show);
        }

        public void SetBiomeTooltipText(string text)
        {
            biomeTooltip.GetComponentInChildren<Text>().text = text;
        }

        public void PositionBiomeTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            biomeTooltip.transform.localPosition += cursorPosition;
        }

        public void ShowAllBiomesTooltip(bool show)
        {
            allBiomesTooltip.SetActive(show);
        }

        public void PositionAllBiomesTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            allBiomesTooltip.transform.localPosition += cursorPosition;
        }

        public void SetAllBiomesTooltipText(string text)
        {
            allBiomesTooltip.GetComponentInChildren<Text>().text = text;
        }

        public void ShowAllResourcesTooltip(bool show)
        {
            allResourcesTooltip.SetActive(show);
        }

        public void PositionAllResourcesTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            allResourcesTooltip.transform.localPosition += cursorPosition;
        }

        public void SetAllResourcesTooltipText(string text)
        {
            allResourcesTooltip.GetComponentInChildren<Text>().text = text;
        }

        public void ShowResourceTooltip(bool show)
        {
            resourceTooltip.SetActive(show);
        }

        public void PositionResourceTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            resourceTooltip.transform.localPosition += cursorPosition;
        }

        public void SetResourceTooltipText(string text)
        {
            resourceTooltip.GetComponentInChildren<Text>().text = text;
        }

        private void SetupImages<T>(
            List<T> objectsToDisplay, 
            List<GameObject> imageObjects, 
            bool unchartedRegion, 
            List<Sprite> imageSprites, 
            GameObject parentObject, 
            GameObject objectPrefab,
            bool displayAll) where T : IFoundableObjectModel
        {
            var objectsNumber = 
                displayAll 
                ? objectsToDisplay.Count : objectsToDisplay.Count > MaxNumberOfObjectsToDisplay 
                ? MaxNumberOfObjectsToDisplay : objectsToDisplay.Count;

            int rarityXPosition;
            double rarityYPosition;
            int xPosition;
            int yPosition;

            for (int i = 0; i < objectsNumber; i++)
            {
                if (!unchartedRegion)
                {
                    if(displayAll)
                    {
                        rarityXPosition = i * ImageOffset + RarityDisplayAllXOffset;
                        xPosition = i * ImageOffset + ImageDisplayAllXOffset;
                        yPosition = 0;
                        rarityYPosition = 0;
                    }
                    else
                    {
                        rarityXPosition = i * ImageOffset + RarityXOffset;
                        xPosition = i * ImageOffset + ImageXOffset;
                        yPosition = -1;
                        rarityYPosition = -1.0;
                    }

                    var rarityObject = Instantiate(rarityImagePrefab, parentObject.transform);

                    rarityObject.transform.localPosition += new Vector3(rarityXPosition, (float)rarityYPosition);

                    var objectToDisplay = Instantiate(objectPrefab, parentObject.transform);

                    objectToDisplay.transform.localPosition += new Vector3(xPosition, yPosition);
                    objectToDisplay.name = objectsToDisplay[i].Name;
                    objectToDisplay.GetComponent<Image>().sprite = imageSprites.FirstOrDefault(bis => bis.name.Contains(objectToDisplay.name));

                    imageObjects.Add(objectToDisplay);

                    rarityObject.GetComponent<Image>().sprite = spritesReader.RarityImageSprites.FirstOrDefault(bis => bis.name.Contains(objectsToDisplay[i].Rarity.ToString()));

                    rarityImageObjects.Add(rarityObject);
                }
                else
                {
                    var objectToDisplay = Instantiate(objectPrefab, parentObject.transform);

                    objectToDisplay.transform.localPosition += new Vector3(i * ImageOffset + UnknownImageOffset, -1);
                    objectToDisplay.name = UnknownString;
                    objectToDisplay.GetComponent<Image>().sprite = spritesReader.BiomeImageSprites.FirstOrDefault(bis => bis.name == UnknownString);

                    imageObjects.Add(objectToDisplay);

                    break;
                }
            }
        }

        private void ClearPanel()
        {
            if (resourceImageObjects.Count > 0)
            {
                foreach (var resourceImage in resourceImageObjects)
                {
                    Destroy(resourceImage);
                }

                resourceImageObjects.Clear();
            }

            if (rarityImageObjects.Count > 0)
            {
                foreach (var rarityImageObject in rarityImageObjects)
                {
                    Destroy(rarityImageObject);
                }

                rarityImageObjects.Clear();
            }

            if (biomeImageObjects.Count > 0)
            {
                foreach (var biomeImage in biomeImageObjects)
                {
                    Destroy(biomeImage);
                }

                biomeImageObjects.Clear();
            }
        }

        //private void SetupResourceImages(List<ResourceModel> resources, bool unchartedRegion)
        //{
        //    var resourcesNumber = resources.Count > MaxNumberOfObjectsToDisplay ? MaxNumberOfObjectsToDisplay : resources.Count;

        //    for (int i = 0; i < resourcesNumber; i++)
        //    {
        //        if (!unchartedRegion)
        //        {
        //            var rarityXPosition = i * 20 + 55;
        //            var resourceXPosition = i * 20 + 57;
        //            var yPosition = -1;

        //            var rarityObject = Instantiate(rarityImagePrefab, this.resources.transform);

        //            rarityObject.transform.localPosition += new Vector3(rarityXPosition, yPosition);

        //            var resourceObject = Instantiate(resourceImagePrefab, this.resources.transform);

        //            resourceObject.transform.localPosition += new Vector3(resourceXPosition, yPosition);
        //            resourceObject.name = resources[i].Name;
        //            resourceObject.GetComponent<Image>().sprite = resourceImageSprites.FirstOrDefault(bis => bis.name.Contains(resourceObject.name));

        //            resourceImageObjects.Add(resourceObject);

        //            rarityObject.GetComponent<Image>().sprite = rarityImageSprites.FirstOrDefault(bis => bis.name.Contains(resources[i].Rarity.ToString()));

        //            rarityImageObjects.Add(rarityObject);
        //        }
        //        else
        //        {
        //            var resourceObject = Instantiate(resourceImagePrefab, this.resources.transform);

        //            resourceObject.transform.localPosition += new Vector3(i * 20 + 57, -1);
        //            resourceObject.name = "Unknown";
        //            resourceObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name == "Unknown");

        //            resourceImageObjects.Add(resourceObject);

        //            break;
        //        }
        //    }
        //}

        //private void SetupBiomeImages(List<BiomeModel> biomes, bool unchartedRegion)
        //{
        //    var biomesNumber = biomes.Count > MaxNumberOfObjectsToDisplay ? MaxNumberOfObjectsToDisplay : biomes.Count;

        //    for (int i = 0; i < biomesNumber; i++)
        //    {
        //        if (!unchartedRegion)
        //        {
        //            var rarityObject = Instantiate(rarityImagePrefab, this.biomes.transform);

        //            rarityObject.transform.localPosition += new Vector3(i * 20 + 55, -1);

        //            var biomeObject = Instantiate(biomeImagePrefab, this.biomes.transform);

        //            biomeObject.transform.localPosition += new Vector3(i * 20 + 57, -1);
        //            biomeObject.name = biomes[i].Name;
        //            biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name.Contains(biomeObject.name));

        //            biomeImageObjects.Add(biomeObject);

        //            rarityObject.GetComponent<Image>().sprite = rarityImageSprites.FirstOrDefault(bis => bis.name.Contains(biomes[i].Rarity.ToString()));

        //            rarityImageObjects.Add(rarityObject);
        //        }
        //        else
        //        {
        //            var biomeObject = Instantiate(biomeImagePrefab, this.biomes.transform);

        //            biomeObject.transform.localPosition += new Vector3(i * 20 + 57, -1);
        //            biomeObject.name = "Unknown";
        //            biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name == "Unknown");

        //            biomeImageObjects.Add(biomeObject);

        //            break;
        //        }
        //    }
        //}
    }
}

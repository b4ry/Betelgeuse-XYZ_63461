using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class RegionSummaryPanelManager : MonoBehaviour, IPanelManager
    {
        public static RegionSummaryPanelManager Instance = null;

        [SerializeField]
        private GameObject regionSummaryPanel;

        [SerializeField]
        private GameObject regionSummaryPanelLabel;
        [SerializeField]
        private GameObject regionSummaryPanelSize;
        [SerializeField]
        private GameObject regionSummaryPanelBiomes;
        [SerializeField]
        private GameObject regionSummaryPanelResources;

        private TextMeshProUGUI regionSummaryPanelLabelTextMesh;
        private TextMeshProUGUI regionSummaryPanelSizeTextMesh;
        private TextMeshProUGUI regionSummaryPanelBiomesTextMesh;
        private TextMeshProUGUI regionSummaryPanelResourcesTextMesh;

        [SerializeField]
        private Button regionSummaryPanelExploreButton;

        [SerializeField]
        private GameObject buttonTooltip;
        [SerializeField]
        private GameObject biomeTooltip;
        [SerializeField]
        private GameObject resourceTooltip;

        [SerializeField]
        private GameObject imagePrefab;
        [SerializeField]
        private GameObject rarityImagePrefab;

        [SerializeField]
        private GameObject oddityImage;

        private List<GameObject> biomeImageObjects = new List<GameObject>();
        private List<GameObject> resourceImageObjects = new List<GameObject>();
        private List<GameObject> rarityImageObjects = new List<GameObject>();

        private List<Sprite> biomeImageSprites = new List<Sprite>();
        private List<Sprite> resourceImageSprites = new List<Sprite>();
        private List<Sprite> rarityImageSprites = new List<Sprite>();
        private List<Sprite> oddityImageSprites = new List<Sprite>();

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

            regionSummaryPanelLabelTextMesh = regionSummaryPanelLabel.GetComponent<TextMeshProUGUI>();
            regionSummaryPanelSizeTextMesh = regionSummaryPanelSize.GetComponent<TextMeshProUGUI>();
            regionSummaryPanelBiomesTextMesh = regionSummaryPanelBiomes.GetComponent<TextMeshProUGUI>();
            regionSummaryPanelResourcesTextMesh = regionSummaryPanelResources.GetComponent<TextMeshProUGUI>();

            //TODO: MOVE TO ASSET BUNDLES
            biomeImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/BiomeSprites").ToList();
            resourceImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/ResourceSprites").ToList();
            rarityImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/RaritySprites").ToList();
            oddityImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/OdditySprites").ToList();
        }

        public void SetActive(bool active)
        {
            regionSummaryPanel.SetActive(active);
        }

        public void SetupRegionSummaryPanel(RegionModel regionModel, bool exploreButtonInteractable)
        {
            var regionName = regionModel.Name;

            if (exploreButtonInteractable)
            {
                regionName = "Uncharted Land";
            }

            regionSummaryPanelLabelTextMesh.SetText(regionName);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + regionModel.Size);
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: ");
            regionSummaryPanelResourcesTextMesh.SetText("Resources: ");

            if (resourceImageObjects.Count > 0)
            {
                foreach (var resourceImage in resourceImageObjects)
                {
                    Destroy(resourceImage);
                }

                resourceImageObjects.Clear();
            }

            if(rarityImageObjects.Count > 0)
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

            SetupBiomeImages(regionModel.Biomes, exploreButtonInteractable);

            var resourcesToDisplay = new List<ResourceModel>();
            
            foreach (var biome in regionModel.Biomes)
            {
                resourcesToDisplay = resourcesToDisplay.Union(biome.Resources).ToList();
            }

            SetupResourceImages(resourcesToDisplay, exploreButtonInteractable);

            if (!exploreButtonInteractable)
            {
                oddityImage.GetComponent<Image>().sprite = oddityImageSprites.FirstOrDefault(ois => ois.name.Equals(regionModel.Oddity.Name));
            }
            else
            {
                oddityImage.GetComponent<Image>().sprite = oddityImageSprites.FirstOrDefault(ois => ois.name.Equals("UnknownOddity"));
            }

            regionSummaryPanelExploreButton.interactable = exploreButtonInteractable;

            if (!regionSummaryPanel.activeSelf)
            {
                regionSummaryPanel.SetActive(true);
            }
        }

        public void AddButtonListener(UnityAction action)
        {
            regionSummaryPanelExploreButton.onClick.RemoveAllListeners();
            regionSummaryPanelExploreButton.onClick.AddListener(action);
        }

        public void ShowButtonTooltip(bool show)
        {
            buttonTooltip.SetActive(show);
        }

        public void PositionButtonTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            buttonTooltip.transform.localPosition += cursorPosition;
        }

        public void ShowBiomeTooltip(bool show)
        {
            biomeTooltip.SetActive(show);
        }

        public void PositionBiomeTooltip(Vector3 cursorPosition)
        {
            cursorPosition.z = 0;
            biomeTooltip.transform.localPosition += cursorPosition;
        }

        public void SetBiomeTooltipText(string text)
        {
            biomeTooltip.GetComponentInChildren<Text>().text = text;
        }

        private void SetupResourceImages(List<ResourceModel> resources, bool unchartedRegion)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                if (!unchartedRegion)
                {
                    var rarityXPosition = i * 20 + 53;
                    var resourceXPosition = i * 20 + 55;
                    var yPosition = -1;

                    if(i > 6)
                    {
                        rarityXPosition = (i-10) * 20 + 60;
                        resourceXPosition = (i-10) * 20 + 62;

                        yPosition = -21;
                    }

                    var rarityObject = Instantiate(rarityImagePrefab, regionSummaryPanelResources.transform);

                    rarityObject.transform.localPosition += new Vector3(rarityXPosition, yPosition);

                    var resourceObject = Instantiate(imagePrefab, regionSummaryPanelResources.transform);

                    resourceObject.transform.localPosition += new Vector3(resourceXPosition, yPosition);
                    resourceObject.name = resources[i].Name;
                    resourceObject.GetComponent<Image>().sprite = resourceImageSprites.FirstOrDefault(bis => bis.name.Contains(resourceObject.name));

                    resourceImageObjects.Add(resourceObject);

                    rarityObject.GetComponent<Image>().sprite = rarityImageSprites.FirstOrDefault(bis => bis.name.Contains(resources[i].RarityEnum.ToString()));

                    rarityImageObjects.Add(rarityObject);
                }
                else
                {
                    var resourceObject = Instantiate(imagePrefab, regionSummaryPanelResources.transform);

                    resourceObject.transform.localPosition += new Vector3(i * 20 + 55, -1);
                    resourceObject.name = "Unknown";
                    resourceObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name == "Unknown");

                    resourceImageObjects.Add(resourceObject);

                    break;
                }
            }
        }

        private void SetupBiomeImages(List<BiomeModel> biomes, bool unchartedRegion)
        {
            for (int i = 0; i < biomes.Count; i++)
            {
                if (!unchartedRegion)
                {
                    var biomeRarityObject = Instantiate(rarityImagePrefab, regionSummaryPanelBiomes.transform);

                    biomeRarityObject.transform.localPosition += new Vector3(i * 20 + 38, -1);

                    var biomeObject = Instantiate(imagePrefab, regionSummaryPanelBiomes.transform);

                    biomeObject.transform.localPosition += new Vector3(i * 20 + 40, -1);
                    biomeObject.name = biomes[i].Name;
                    biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name.Contains(biomeObject.name));

                    biomeImageObjects.Add(biomeObject);

                    biomeRarityObject.GetComponent<Image>().sprite = rarityImageSprites.FirstOrDefault(bis => bis.name.Contains(biomes[i].Rarity.ToString()));

                    rarityImageObjects.Add(biomeRarityObject);
                }
                else
                {
                    var biomeObject = Instantiate(imagePrefab, regionSummaryPanelBiomes.transform);

                    biomeObject.transform.localPosition += new Vector3(i * 20 + 40, -1);
                    biomeObject.name = "Unknown";
                    biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name == "Unknown");

                    biomeImageObjects.Add(biomeObject);

                    break;
                }
            }
        }
    }
}

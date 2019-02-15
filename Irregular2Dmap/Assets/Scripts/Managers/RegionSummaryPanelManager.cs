using Assets.Scripts.Controllers;
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

        private TextMeshProUGUI regionSummaryPanelLabelTextMesh;
        private TextMeshProUGUI regionSummaryPanelSizeTextMesh;
        private TextMeshProUGUI regionSummaryPanelBiomesTextMesh;

        [SerializeField]
        private Button regionSummaryPanelExploreButton;

        [SerializeField]
        private GameObject buttonTooltip;
        [SerializeField]
        private GameObject biomeTooltip;

        [SerializeField]
        private GameObject biomeImagePrefab;
        [SerializeField]
        private GameObject biomeRarityImagePrefab;

        [SerializeField]
        private GameObject oddityImage;

        private List<GameObject> biomeImageObjects = new List<GameObject>();
        private List<GameObject> biomeRarityImageObjects = new List<GameObject>();

        private List<Sprite> biomeImageSprites = new List<Sprite>();
        private List<Sprite> biomeRarityImageSprites = new List<Sprite>();
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

            //TODO: MOVE TO ASSET BUNDLES
            biomeImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/BiomeSprites").ToList();
            biomeRarityImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/BiomeSprites/Rarity").ToList();
            oddityImageSprites = Resources.LoadAll<Sprite>("UI/RegionSummaryPanel/OdditySprites").ToList();
        }

        public void SetActive(bool active)
        {
            regionSummaryPanel.SetActive(active);      
        }

        public void SetupRegionSummaryPanel(RegionModel regionModel, bool exploreButtonInteractable)
        {
            var regionName = regionModel.Name;

            if(exploreButtonInteractable)
            {
                regionName = "Uncharted Land";
            }

            regionSummaryPanelLabelTextMesh.SetText(regionName);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + regionModel.Size);
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: ");

            SetupBiomeImages(regionModel.Biomes, exploreButtonInteractable);

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

        private void SetupBiomeImages(List<BiomeModel> biomes, bool unchartedRegion)
        {
            if (biomeImageObjects.Count > 0)
            {
                foreach (var biomeImage in biomeImageObjects)
                {
                    Destroy(biomeImage);
                }

                foreach(var biomeRarityImage in biomeRarityImageObjects)
                {
                    Destroy(biomeRarityImage);
                }

                biomeImageObjects.Clear();
                biomeRarityImageObjects.Clear();
            }

            for (int i = 0; i < biomes.Count; i++)
            {
                if (!unchartedRegion)
                {
                    var biomeRarityObject = Instantiate(biomeRarityImagePrefab, regionSummaryPanelBiomes.transform);

                    biomeRarityObject.transform.localPosition += new Vector3(i * 20 + 38, -1);

                    var biomeObject = Instantiate(biomeImagePrefab, regionSummaryPanelBiomes.transform);

                    biomeObject.transform.localPosition += new Vector3(i * 20 + 40, -1);
                    biomeObject.name = biomes[i].Biome.ToString();
                    biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name.Contains(biomeObject.name));

                    biomeImageObjects.Add(biomeObject);

                    biomeRarityObject.GetComponent<Image>().sprite = biomeRarityImageSprites.FirstOrDefault(bis => bis.name.Contains(biomes[i].Rarity.ToString()));

                    biomeRarityImageObjects.Add(biomeRarityObject);
                }
                else
                {
                    var biomeObject = Instantiate(biomeImagePrefab, regionSummaryPanelBiomes.transform);

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

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

        private List<GameObject> biomeImageObjects = new List<GameObject>();
        private List<Sprite> biomeImageSprites = new List<Sprite>();

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
        }

        public void SetActive(bool active)
        {
            regionSummaryPanel.SetActive(active);      
        }

        public void SetupRegionSummaryPanel(string regionName, string size, string biomes, bool exploreButtonInteractable)
        {
            regionSummaryPanelLabelTextMesh.SetText(regionName);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + size);
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: ");

            SetupBiomeImages(biomes);

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

        private void SetupBiomeImages(string biomes)
        {
            if (biomeImageObjects.Count > 0)
            {
                foreach (var biomeImage in biomeImageObjects)
                {
                    Destroy(biomeImage);
                }

                biomeImageObjects.Clear();
            }

            var biomesArray = biomes.Replace(" ", "").Split(',');

            for (int i = 0; i < biomesArray.Length; i++)
            {
                var biomeObject = Instantiate(biomeImagePrefab, regionSummaryPanelBiomes.transform);
                biomeObject.transform.localPosition += new Vector3(i * 20 + 60, 0);

                biomeObject.name = biomesArray[i];

                if (biomesArray[i] != "???")
                {
                    var biome = biomesArray[i];
                    biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name.Contains(biomesArray[i]));
                }
                else
                {
                    biomeObject.GetComponent<Image>().sprite = biomeImageSprites.FirstOrDefault(bis => bis.name == "Unknown");
                }

                biomeImageObjects.Add(biomeObject);
            }
        }
    }
}

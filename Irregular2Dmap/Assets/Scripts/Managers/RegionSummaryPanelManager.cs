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
        private GameObject toolTip;

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
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);      
        }

        public void SetupRegionSummaryPanel(string regionName, string size, string biomes, bool exploreButtonInteractable)
        {
            regionSummaryPanelLabelTextMesh.SetText(regionName);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + size);
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: " + biomes);

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

        public void ShowToolTip(bool show)
        {
            toolTip.SetActive(show);
        }
    }
}

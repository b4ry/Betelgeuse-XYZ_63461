using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel
{
    public class BiomeDetailsPanelController : MonoBehaviour
    {
        private const int PrefabOffset = -100;
        private const string FloatFormat = "F";
        private const string PercentageSymbol = "%";

        public GameObject BiomeImage;
        public GameObject BiomeArea;
        public GameObject BiomeName;
        public GameObject BiomeRarity;
        public GameObject ResourceCollection;

        public GameObject ResourcePrefab;

        private TextMeshProUGUI biomeAreaTextMesh;
        private TextMeshProUGUI biomeNameTextMesh;
        private TextMeshProUGUI biomeRarityTextMesh;

        void Awake()
        {
            biomeAreaTextMesh = BiomeArea.GetComponent<TextMeshProUGUI>();
            biomeNameTextMesh = BiomeName.GetComponent<TextMeshProUGUI>();
            biomeRarityTextMesh = BiomeRarity.GetComponent<TextMeshProUGUI>();
        }

        public void SetupPanel(BiomeModel biomeModel, SpritesReader spritesReader)
        {
            BiomeImage.GetComponent<Image>().sprite = spritesReader.BiomeImageSprites.FirstOrDefault(bis => bis.name == biomeModel.Name);

            biomeAreaTextMesh.text = biomeModel.AreaPercentage.ToString(FloatFormat) + PercentageSymbol;
            biomeNameTextMesh.text = biomeModel.Name;
            biomeRarityTextMesh.text = biomeModel.Rarity.ToString();

            int drawnResourcesNumber = 0;

            foreach(var resourceModel in biomeModel.Resources)
            {
                if (resourceModel.IsAvailable)
                {
                    var resource = Instantiate(ResourcePrefab, ResourceCollection.transform);
                    var resourceDetailsPanelController = resource.GetComponent<ResourceDetailsPanelController>();

                    resourceDetailsPanelController.SetupPanel(resourceModel, spritesReader);
                    resource.transform.localPosition += new Vector3(0, drawnResourcesNumber * PrefabOffset);

                    ++drawnResourcesNumber;

                    ResourceCollection.GetComponent<RectTransform>().sizeDelta = new Vector2(0, drawnResourcesNumber * 100);
                }
            }
        }
    }
}

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
        private const int PrefabOffset = -20;
        private const string FloatFormat = "F";
        private const string PercentageSymbol = "%";

        public GameObject BiomeImage;
        public GameObject BiomeArea;
        public GameObject BiomeName;
        public GameObject BiomeRarity;
        public GameObject ResourceCollection;

        public GameObject ResourceImagePrefab;
        public GameObject ResourceAreaPrefab;

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

            for(int i = 0; i < biomeModel.Resources.Count; i++)
            {
                var resourceImage = Instantiate(ResourceImagePrefab, ResourceCollection.transform);
                var resourceArea = Instantiate(ResourceAreaPrefab, ResourceCollection.transform);

                resourceImage.transform.localPosition += new Vector3(0, i * PrefabOffset);
                resourceArea.transform.localPosition += new Vector3(0, i * PrefabOffset);

                resourceImage.GetComponent<Image>().sprite = spritesReader.ResourceImageSprites.FirstOrDefault(ris => ris.name == biomeModel.Resources[i].Name);
                resourceArea.GetComponent<TextMeshProUGUI>().text = biomeModel.Resources[i].DepositAmount.ToString(FloatFormat);
            }
        }
    }
}

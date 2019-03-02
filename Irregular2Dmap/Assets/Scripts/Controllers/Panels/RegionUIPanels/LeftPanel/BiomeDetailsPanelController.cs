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
        public GameObject BiomeImage;
        public GameObject BiomeArea;
        public GameObject BiomeName;
        public GameObject BiomeRarity;

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

            biomeAreaTextMesh.text = biomeModel.AreaPercentage.ToString("F") + "%";
            biomeNameTextMesh.text = biomeModel.Name;
            biomeRarityTextMesh.text = biomeModel.Rarity.ToString();
        }
    }
}

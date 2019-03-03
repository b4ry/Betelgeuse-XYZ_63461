using Assets.Scripts.Readers;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationPanelController : MonoBehaviour
    {
        private const int BiomeButtonXOffset = 110;

        public GameObject Sprites;
        public GameObject BiomesPanel;
        public GameObject BiomeButtonPrefab;
        
        private SpritesReader spritesReader;

        void Awake()
        {
            spritesReader = Sprites.GetComponent<SpritesReader>();
        }

        public void SetupPanel()
        {
            var regionModel = SelectedRegionsController.Instance.SelectedRegionObjects.FirstOrDefault().RegionModel;
            var drawnBiomesNumber = 0;

            foreach (var biome in regionModel.Biomes)
            {
                var biomeButton = Instantiate(BiomeButtonPrefab, BiomesPanel.transform);

                biomeButton.GetComponent<BiomeButtonController>().SetupButton(biome, spritesReader);
                biomeButton.transform.localPosition += new Vector3(drawnBiomesNumber * BiomeButtonXOffset, 0);

                ++drawnBiomesNumber;
            }
        }
    }
}

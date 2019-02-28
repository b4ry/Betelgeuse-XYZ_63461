using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels
{
    public class TopPanelController : MonoBehaviour
    {
        public static TopPanelController Instance = null;

        [SerializeField]
        private GameObject sprites;
        [SerializeField]
        private GameObject regionName;
        [SerializeField]
        private GameObject oddityImage;

        private TextMeshProUGUI nameTextMesh;
        private SpritesReader spritesReader;

        private RegionModel regionModel;

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
            spritesReader = sprites.GetComponent<SpritesReader>();
        }

        public  void GoBackToWorldMap()
        {
            GameController.Instance.LoadWorldMapView();
        }

        public void SetupTopPanel()
        {
            regionModel = SelectedRegionsController.Instance.SelectedRegionObjects.FirstOrDefault().RegionModel;

            nameTextMesh.SetText(regionModel.Name);
            oddityImage.GetComponent<Image>().sprite = spritesReader.OddityImageSprites.FirstOrDefault(ois => ois.name.Equals(regionModel.Oddity.Name));
        }
    }
}

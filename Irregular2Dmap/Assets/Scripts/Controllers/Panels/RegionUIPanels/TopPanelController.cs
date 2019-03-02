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

        private TextMeshProUGUI regionNameTextMesh;

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

            regionNameTextMesh = regionName.GetComponent<TextMeshProUGUI>();
        }

        public void SetupPanel(RegionModel regionModel, SpritesReader spritesReader)
        {
            regionNameTextMesh.SetText(regionModel.Name);
            oddityImage.GetComponent<Image>().sprite = spritesReader.OddityImageSprites.FirstOrDefault(ois => ois.name.Equals(regionModel.Oddity.Name));
        }
    }
}

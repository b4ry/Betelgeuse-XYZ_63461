using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUI
{
    public class TopPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject sprites;
        [SerializeField]
        private GameObject regionName;
        [SerializeField]
        private GameObject oddityImage;

        private TextMeshProUGUI regionNameTextMesh;

        void Awake()
        {
            regionNameTextMesh = regionName.GetComponent<TextMeshProUGUI>();
        }

        public void SetupPanel(RegionModel regionModel, SpritesReader spritesReader)
        {
            regionNameTextMesh.SetText(regionModel.Name);
            oddityImage.GetComponent<Image>().sprite = spritesReader.OddityImageSprites.FirstOrDefault(ois => ois.name.Equals(regionModel.Oddity.Name));
        }
    }
}

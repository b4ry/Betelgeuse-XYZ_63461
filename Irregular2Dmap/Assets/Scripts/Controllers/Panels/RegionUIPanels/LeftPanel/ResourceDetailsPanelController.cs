using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel
{
    public class ResourceDetailsPanelController : MonoBehaviour
    {
        private const string FloatFormat = "F";

        public GameObject ResourceImage;
        public GameObject Name;
        public GameObject Units;
        public GameObject Rarity;

        public void SetupPanel(ResourceModel resourceModel, SpritesReader spritesReader)
        {
            ResourceImage.GetComponent<Image>().sprite = spritesReader.ResourceImageSprites.FirstOrDefault(ris => ris.name == resourceModel.Name);
            Name.GetComponent<TextMeshProUGUI>().text = resourceModel.Name;
            Units.GetComponent<TextMeshProUGUI>().text = resourceModel.DepositAmount.ToString(FloatFormat);
            Rarity.GetComponent<TextMeshProUGUI>().text = resourceModel.Rarity.ToString();
        }
    }
}

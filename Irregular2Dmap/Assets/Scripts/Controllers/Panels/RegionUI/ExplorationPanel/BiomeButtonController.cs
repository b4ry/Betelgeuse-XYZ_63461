using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUI.ExplorationPanel
{
    public class BiomeButtonController : MonoBehaviour
    {
        public GameObject BiomeImage;
        public GameObject BiomeName;

        public void SetupButton(BiomeModel biomeModel, SpritesReader spritesReader, ExplorationGameAreaPanelController explorationGameAreaPanelController)
        {
            BiomeImage.GetComponent<Image>().sprite = spritesReader.BiomeImageSprites.FirstOrDefault(bis => bis.name == biomeModel.Name);
            BiomeName.GetComponent<Text>().text = biomeModel.Name;

            var button = gameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate {
                explorationGameAreaPanelController.SetupPanel(biomeModel);
            });
        }
    }
}

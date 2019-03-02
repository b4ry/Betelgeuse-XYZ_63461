using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel
{
    public class LeftPanelController : MonoBehaviour
    {
        public GameObject Biomes;
        public GameObject BiomeDetailsPrefab;

        //[SerializeField]
        //private GameObject resourcesCollection;

        public void SetupPanel(RegionModel regionModel)
        {
            var biomesPanelSize = Biomes.GetComponent<RectTransform>().sizeDelta;
            biomesPanelSize = new Vector2(303, 0);

            foreach (var biome in regionModel.Biomes)
            {
                biomesPanelSize += new Vector2(0, 158);
            }

            Biomes.GetComponent<RectTransform>().sizeDelta = biomesPanelSize;
        }

        //public void HandleResourcesScrollbar(float newValue)
        //{
        //    var position = resourcesCollection.transform.localPosition;

        //    position.y = newValue;
        //    resourcesCollection.transform.localPosition = position;
        //}
    }
}

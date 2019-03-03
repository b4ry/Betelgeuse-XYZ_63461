using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel
{
    public class LeftPanelController : MonoBehaviour
    {
        public GameObject Biomes;
        public GameObject BiomeDetailsPrefab;

        private List<GameObject> biomeDetailsObjects = new List<GameObject>();

        public void SetupPanel(RegionModel regionModel, SpritesReader spritesReader)
        {
            if (biomeDetailsObjects.Count > 0)
            {
                foreach (var biomeDetailsObject in biomeDetailsObjects)
                {
                    Destroy(biomeDetailsObject);
                }

                biomeDetailsObjects.Clear();
            }

            var biomesPanelSize = Biomes.GetComponent<RectTransform>().sizeDelta;
            biomesPanelSize = new Vector2(303, 0);

            for (int i = 0; i < regionModel.Biomes.Count; i++)
            {
                biomesPanelSize += new Vector2(0, 158);

                var biomeDetails = Instantiate(BiomeDetailsPrefab, Biomes.transform);
                biomeDetails.transform.localPosition += new Vector3(0, i * 158 * (-1));

                biomeDetails.GetComponent<BiomeDetailsPanelController>().SetupPanel(regionModel.Biomes[i], spritesReader);

                biomeDetailsObjects.Add(biomeDetails);
            }

            Biomes.GetComponent<RectTransform>().sizeDelta = biomesPanelSize;
        }
    }
}

using Assets.Scripts.Controllers;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers.Region
{
    public class TopPanelManager : MonoBehaviour
    {
        public static TopPanelManager Instance = null;

        [SerializeField]
        private GameObject regionName;

        private TextMeshProUGUI nameTextMesh;

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
        }

        public  void GoBackToWorldMap()
        {
            GameController.Instance.LoadWorldMapView();
        }

        public void SetupTopPanel()
        {
            nameTextMesh.SetText(SelectedRegionsController.Instance.SelectedRegionObjects.FirstOrDefault().RegionModel.Name);
        }
    }
}

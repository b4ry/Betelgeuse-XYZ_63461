using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SelectedRegionsController : MonoBehaviour
    {
        public static SelectedRegionsController Instance = null;

        public List<RegionController> SelectedRegionObjects = new List<RegionController>();

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
        }
    }
}

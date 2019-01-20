using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SelectedRegionsController : MonoBehaviour
    {
        public static SelectedRegionsController instance = null;

        public List<RegionController> SelectedRegionObjects = new List<RegionController>();

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}

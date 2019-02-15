using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance = null;

        public List<GameObject> Regions = new List<GameObject>();
        public GameObject MainCamera;

        public System.Random RNG;

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

            RNG = new System.Random();
            var initialRegion = Regions[RNG.Next(0, Regions.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();
        }

        private void Start()
        {
        }
    }
}

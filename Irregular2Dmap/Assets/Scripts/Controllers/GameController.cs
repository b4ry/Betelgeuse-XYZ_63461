using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance = null;

        public List<GameObject> RegionObjects = new List<GameObject>();

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
            var initialRegion = RegionObjects[RNG.Next(0, RegionObjects.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();
        }

        private void Start()
        {
        }
    }
}

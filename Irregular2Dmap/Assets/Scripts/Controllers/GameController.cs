using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance = null;

        public List<GameObject> Regions = new List<GameObject>();

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

            var random = new System.Random();
            var initialRegion = Regions[random.Next(0, Regions.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();
        }

        private void Start()
        {
        }
    }
}

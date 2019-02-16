using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance = null;

        public GameObject MainCamera;

        public List<GameObject> RegionObjects = new List<GameObject>();
        public List<Sprite> RegionNFOWSprites = new List<Sprite>();
        public List<Sprite> RegionNFOWOutlineSprites = new List<Sprite>();
        public List<Sprite> RegionFOWSprites = new List<Sprite>();
        public List<Sprite> RegionFOWOutlineSprites = new List<Sprite>();

        public System.Random RNG { get; set; }
        public string MapName { get; set; }

        [SerializeField]
        private GameObject regionPrefab;
        [SerializeField]
        private GameObject regionsObject;

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

            // TODO: ASSET BUNDLE
            RegionNFOWSprites = Resources.LoadAll<Sprite>("Maps/Regions/NFOWs").ToList();
            RegionNFOWOutlineSprites = Resources.LoadAll<Sprite>("Maps/Regions/NFOWOutlines").ToList();
            RegionFOWSprites = Resources.LoadAll<Sprite>("Maps/Regions/FOWs").ToList();
            RegionFOWOutlineSprites = Resources.LoadAll<Sprite>("Maps/Regions/FOWOutlines").ToList();

            MapName = "Map1";
            BuildMapFromItsDefinition();

            RNG = new System.Random();

            var initialRegion = RegionObjects[RNG.Next(0, RegionObjects.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();

            DontDestroyOnLoad(gameObject);
        }

        private void BuildMapFromItsDefinition()
        {
            var path = $"/Assets/MapsDefinition/{MapName}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] mapDefinition = File.ReadAllLines(fullPath);

            for(int i = 1; i <= int.Parse(mapDefinition[0]); i++)
            {
                var regionObject = Instantiate(regionPrefab, regionsObject.transform);

                regionObject.name = string.Concat("Region", i);

                var regionController = regionObject.GetComponent<RegionController>();
                regionController.regionSprite = RegionNFOWSprites.FirstOrDefault(rnfows => rnfows.name.Contains(regionObject.name));
                regionController.regionOutlineSprite = RegionNFOWOutlineSprites.FirstOrDefault(rnfowos => rnfowos.name.Contains(regionObject.name));
                regionController.regionFogOfWarSprite = RegionFOWSprites.FirstOrDefault(rfows => rfows.name.Contains(regionObject.name));
                regionController.regionFogOfWarOutlineSprite = RegionFOWOutlineSprites.FirstOrDefault(rfowos => rfowos.name.Contains(regionObject.name));

                regionObject.SetActive(true);

                RegionObjects.Add(regionObject);
            }
        }

        private void Start()
        {
        }
    }
}

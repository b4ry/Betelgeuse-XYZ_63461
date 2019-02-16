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

        public System.Random RNG { get; set; }
        public string MapName { get; set; }

        [SerializeField]
        private GameObject worldMapObject;
        [SerializeField]
        private GameObject regionPrefab;
        [SerializeField]
        private GameObject regionsObject;

        private List<Sprite> worldMapSprites = new List<Sprite>();
        private List<Sprite> regionNFOWSprites = new List<Sprite>();
        private List<Sprite> regionNFOWOutlineSprites = new List<Sprite>();
        private List<Sprite> regionFOWSprites = new List<Sprite>();
        private List<Sprite> regionFOWOutlineSprites = new List<Sprite>();

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
            worldMapSprites = Resources.LoadAll<Sprite>("Maps/Worlds").ToList();
            regionNFOWSprites = Resources.LoadAll<Sprite>("Maps/Regions/NFOWs").ToList();
            regionNFOWOutlineSprites = Resources.LoadAll<Sprite>("Maps/Regions/NFOWOutlines").ToList();
            regionFOWSprites = Resources.LoadAll<Sprite>("Maps/Regions/FOWs").ToList();
            regionFOWOutlineSprites = Resources.LoadAll<Sprite>("Maps/Regions/FOWOutlines").ToList();

            BuildMapFromItsDefinition();

            RNG = new System.Random();

            var initialRegion = RegionObjects[RNG.Next(0, RegionObjects.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();

            DontDestroyOnLoad(gameObject);
        }

        private void BuildMapFromItsDefinition()
        {
            MapName = "Map1";

            var path = $"/Assets/MapsDefinition/{MapName}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] mapDefinition = File.ReadAllLines(fullPath);

            var worldMapName = mapDefinition[0];
            var regionsNumber = mapDefinition[1];

            worldMapObject.GetComponent<SpriteRenderer>().sprite = worldMapSprites.FirstOrDefault(wms => wms.name.Contains(worldMapName));

            for(int i = 1; i <= int.Parse(regionsNumber); i++)
            {
                var regionObject = Instantiate(regionPrefab, regionsObject.transform);

                regionObject.name = string.Concat("Region", i);

                var regionController = regionObject.GetComponent<RegionController>();
                regionController.RegionSprite = regionNFOWSprites.FirstOrDefault(rnfows => rnfows.name.Contains(regionObject.name));
                regionController.RegionOutlineSprite = regionNFOWOutlineSprites.FirstOrDefault(rnfowos => rnfowos.name.Contains(regionObject.name));
                regionController.RegionFogOfWarSprite = regionFOWSprites.FirstOrDefault(rfows => rfows.name.Contains(regionObject.name));
                regionController.RegionFogOfWarOutlineSprite = regionFOWOutlineSprites.FirstOrDefault(rfowos => rfowos.name.Contains(regionObject.name));

                regionObject.SetActive(true);

                RegionObjects.Add(regionObject);
            }
        }

        private void Start()
        {
        }
    }
}

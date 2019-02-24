using Assets.Scripts.Readers;
using System.Collections.Generic;
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
        [SerializeField]
        private GameObject worldMapUiCanvas;

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

            // TODO: WILL BE READ FROM STARTING MENU
            MapName = "Map1";

            // TODO: ASSET BUNDLE
            worldMapSprites = Resources.LoadAll<Sprite>("Maps/Worlds").ToList();
            regionNFOWSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/NFOWs").ToList();
            regionNFOWOutlineSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/NFOWOutlines").ToList();
            regionFOWSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/FOWs").ToList();
            regionFOWOutlineSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/FOWOutlines").ToList();

            BuildMapFromItsDefinition();

            RNG = new System.Random();

            var initialRegion = RegionObjects[RNG.Next(0, RegionObjects.Count)];

            initialRegion.GetComponent<RegionController>().SetInitial();

            DontDestroyOnLoad(gameObject);
        }

        private void BuildMapFromItsDefinition()
        {
            var path = $"/Assets/Definitions/MapsDefinition/{MapName}.txt";

            string[] mapDefinition = FileReader.ReadFile(path);

            var worldMapName = mapDefinition[0];
            var regionsNumber = mapDefinition[1];
            var regionNames = mapDefinition[2].Split(';');

            worldMapObject.GetComponent<SpriteRenderer>().sprite = worldMapSprites.FirstOrDefault(wms => wms.name.Contains(worldMapName));

            for(int i = 0; i < int.Parse(regionsNumber); i++)
            {
                var regionObject = Instantiate(regionPrefab, regionsObject.transform);

                regionObject.name = regionNames[i];

                var regionController = regionObject.GetComponent<RegionController>();
                regionController.RegionSprite = regionNFOWSprites.FirstOrDefault(rnfows => rnfows.name.Contains(regionObject.name));
                regionController.RegionOutlineSprite = regionNFOWOutlineSprites.FirstOrDefault(rnfowos => rnfowos.name.Contains(regionObject.name));
                regionController.RegionFogOfWarSprite = regionFOWSprites.FirstOrDefault(rfows => rfows.name.Contains(regionObject.name));
                regionController.RegionFogOfWarOutlineSprite = regionFOWOutlineSprites.FirstOrDefault(rfowos => rfowos.name.Contains(regionObject.name));

                regionObject.SetActive(true);

                RegionObjects.Add(regionObject);
            }
        }

        public void LoadRegionView()
        {
            worldMapObject.SetActive(false);
            regionsObject.SetActive(false);
            worldMapUiCanvas.SetActive(false);
        }

        private void Start()
        {
        }
    }
}

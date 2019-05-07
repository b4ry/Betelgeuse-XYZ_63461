using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using Assets.Scripts.Managers.Player;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
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
        public GameObject Definitions;

        public List<GameObject> RegionObjects = new List<GameObject>();

        public System.Random Rng { get; set; }
        public string MapName { get; set; }
        public PlayerModel ActivePlayer { get; set; }

        [SerializeField]
        private GameObject worldMapObject;
        [SerializeField]
        private GameObject regionPrefab;
        [SerializeField]
        private GameObject regionsObject;
        [SerializeField]
        private GameObject worldMapUICanvas;
        [SerializeField]
        private GameObject regionUICanvas;
        [SerializeField]
        private GameObject regionTopPanel;

        private List<Sprite> worldMapSprites = new List<Sprite>();
        private List<Sprite> regionNFOWSprites = new List<Sprite>();
        private List<Sprite> regionNFOWOutlineSprites = new List<Sprite>();
        private List<Sprite> regionFOWSprites = new List<Sprite>();
        private List<Sprite> regionFOWOutlineSprites = new List<Sprite>();

        private IFactory<IPlayerManager> playerFactory;
        private DefinitionsController definitionsController;
        private int currentPlayerIndex = 0;

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

            MapName = GameInfoStorageController.Instance.MapName;

            // TODO: ASSET BUNDLE
            worldMapSprites = Resources.LoadAll<Sprite>("Maps/Worlds").ToList();
            regionNFOWSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/NFOWs").ToList();
            regionNFOWOutlineSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/NFOWOutlines").ToList();
            regionFOWSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/FOWs").ToList();
            regionFOWOutlineSprites = Resources.LoadAll<Sprite>($"Maps/Regions/{MapName}/FOWOutlines").ToList();

            BuildMapFromItsDefinition();

            Rng = new System.Random();

            definitionsController = Definitions.GetComponent<DefinitionsController>();

            DontDestroyOnLoad(gameObject);

            foreach (var player in GameInfoStorageController.Instance.Players)
            {
                Debug.Log($"{player.Nickname} : {player.Race}");
            }
        }

        void Start()
        {
            foreach (var regionObject in RegionObjects)
            {
                var regionController = regionObject.GetComponent<RegionController>();

                regionController.DefineModel();
            }

            foreach (var player in GameInfoStorageController.Instance.Players)
            {
                definitionsController.ReadBuildingsDefinitions(player);
                ProducePlayer(player);

                var initialPlayerRegion = RegionObjects[Rng.Next(0, RegionObjects.Count)];

                player.PlayerManager.InitialRegion = initialPlayerRegion;
            }

            ActivePlayer = GameInfoStorageController.Instance.Players.FirstOrDefault();

            var activeRegionController = ActivePlayer.PlayerManager.InitialRegion.GetComponent<RegionController>();
            activeRegionController.SetActive();
        }

        private void BuildMapFromItsDefinition()
        {
            var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Maps/{MapName}.xml";
            var mapDefinition = MapDefinitionModel.Load(path);

            worldMapObject.GetComponent<SpriteRenderer>().sprite = worldMapSprites.FirstOrDefault(wms => wms.name.Contains(mapDefinition.Name));

            for (int i = 0; i < mapDefinition.RegionsNumber; i++)
            {
                var regionObject = Instantiate(regionPrefab, regionsObject.transform);

                regionObject.name = mapDefinition.Regions[i].Name;

                var regionController = regionObject.GetComponent<RegionController>();
                regionController.RegionSprite = regionNFOWSprites.FirstOrDefault(rnfows => rnfows.name.Contains(regionObject.name));
                regionController.RegionOutlineSprite = regionNFOWOutlineSprites.FirstOrDefault(rnfowos => rnfowos.name.Contains(regionObject.name));
                regionController.RegionFogOfWarSprite = regionFOWSprites.FirstOrDefault(rfows => rfows.name.Contains(regionObject.name));
                regionController.RegionFogOfWarOutlineSprite = regionFOWOutlineSprites.FirstOrDefault(rfowos => rfowos.name.Contains(regionObject.name));

                regionObject.SetActive(true);

                RegionObjects.Add(regionObject);
            }
        }

        private void ProducePlayer(PlayerModel player)
        {
            playerFactory = new PlayerFactory();
            player.PlayerManager = playerFactory.Produce(player);
            player.PlayerManager.Race = player.Race;
        }

        public void LoadRegionView()
        {
            worldMapObject.SetActive(false);
            regionsObject.SetActive(false);
            worldMapUICanvas.SetActive(false);

            regionUICanvas.SetActive(true);
            regionUICanvas.GetComponent<RegionUIController>().SetupView();
        }

        public void LoadWorldMapView()
        {
            worldMapObject.SetActive(true);
            regionsObject.SetActive(true);
            worldMapUICanvas.SetActive(true);

            regionUICanvas.SetActive(false);
        }

        public void EndTurn()
        {
            ++currentPlayerIndex;

            if (currentPlayerIndex > GameInfoStorageController.Instance.Players.Count - 1)
            {
                currentPlayerIndex = 0;
            }

            ActivePlayer = GameInfoStorageController.Instance.Players[currentPlayerIndex];

            foreach (var regionObject in RegionObjects)
            {
                var regionController = regionObject.GetComponent<RegionController>();

                regionController.PlaceFogOfWar();
            }

            var activeRegionController = ActivePlayer.PlayerManager.InitialRegion.GetComponent<RegionController>();
            activeRegionController.SetActive();
        }
    }
}

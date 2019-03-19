using Assets.Scripts.Enums;
using Assets.Scripts.Models.Definitions;
using Assets.Scripts.Readers;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.MainMenu
{
    public class MainMenuPanelController : MonoBehaviour
    {
        private readonly string AvailableMapsPath = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Maps/AvailableMaps.xml";

        public GameObject MapsDropdownObject;
        public GameObject RacesDropdownObject;
        public GameObject GameInfoStorageObject;

        private Dropdown mapsDropdown;
        private Dropdown racesDropdown;
        private GameInfoStorageController gameInfoStorageController;

        void Awake()
        {
            mapsDropdown = MapsDropdownObject.GetComponent<Dropdown>();
            racesDropdown = RacesDropdownObject.GetComponent<Dropdown>();

            ReadMaps();
            ReadRaces();

            var gameInfoStorageController = GameInfoStorageObject.GetComponent<GameInfoStorageController>();

            gameInfoStorageController.Race = (RaceEnum)Enum.Parse(typeof(RaceEnum), racesDropdown.options.FirstOrDefault().text);
            gameInfoStorageController.MapName = mapsDropdown.options.FirstOrDefault().text;
        }

        private void ReadMaps()
        {
            var availableMaps = AvailableMapsDefinitionModel.Load(AvailableMapsPath);

            foreach(var availableMap in availableMaps.Maps)
            {
                var dropdownOption = new Dropdown.OptionData(availableMap.Name);

                mapsDropdown.options.Add(dropdownOption);
            }
        }

        private void ReadRaces()
        {
            var path = $"/Assets/Definitions/Races/AvailableRaces.txt";
            string[] availableRaces = FileReader.ReadFile(path, true);

            foreach (var availableRace in availableRaces)
            {
                var dropdownOption = new Dropdown.OptionData(availableRace);

                racesDropdown.options.Add(dropdownOption);
            }
        }
    }
}

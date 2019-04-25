using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MainMenuActionsController : MonoBehaviour
    {
        private readonly string AvailableRacesPath = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Races/AvailableRaces.xml";
        private readonly string GameScene = "GameScene";

        public GameObject GameInfoStorageObject;
        public GameObject RaceDropdownPrefab;
        public GameObject MainMenuPanelObject;

        private GameInfoStorageController gameInfoStorageController;
        private List<GameObject> newRaceDropdowns = new List<GameObject>();
        private List<RaceDefinitionModel> availableRaces;

        void Awake()
        {
            gameInfoStorageController = GameInfoStorageObject.GetComponent<GameInfoStorageController>();
            availableRaces = AvailableRacesDefinitionModel.Load(AvailableRacesPath).Races;

            var dropdownOptions = RaceDropdownPrefab.GetComponent<Dropdown>().options;
            dropdownOptions.Clear();

            foreach (var availableRace in availableRaces)
            {
                var dropdownOption = new Dropdown.OptionData(availableRace.Name);

                dropdownOptions.Add(dropdownOption);
            }

            var initialRacesDropdown = Instantiate(RaceDropdownPrefab, MainMenuPanelObject.transform);
            initialRacesDropdown.transform.localPosition += new Vector3(0, 0);

            newRaceDropdowns.Add(initialRacesDropdown);
        }

        public void PickMap(Dropdown mapsDropdown)
        {
            gameInfoStorageController.MapName = mapsDropdown.options[mapsDropdown.value].text;
        }

        public void SetNumberOfPlayers(Slider playersNumberSlider)
        {
            gameInfoStorageController.PlayersNumber = (int) playersNumberSlider.value;

            if(gameInfoStorageController.PlayersNumber > 1 && gameInfoStorageController.PlayersNumber > newRaceDropdowns.Count)
            {
                var dropdownsToAdd = gameInfoStorageController.PlayersNumber - newRaceDropdowns.Count;

                for (int i = 0; i < dropdownsToAdd; i++)
                {
                    var racesDropdown = Instantiate(RaceDropdownPrefab, MainMenuPanelObject.transform);
                    racesDropdown.transform.localPosition += new Vector3(0, newRaceDropdowns.Count * (-40));

                    newRaceDropdowns.Add(racesDropdown);
                }
            } 
            else if(gameInfoStorageController.PlayersNumber == 1 || gameInfoStorageController.PlayersNumber < newRaceDropdowns.Count)
            {
                var dropdownsToRemove = (newRaceDropdowns.Count) - gameInfoStorageController.PlayersNumber;

                for(var i = 0; i < dropdownsToRemove; i++)
                {
                    var indexLast = newRaceDropdowns.Count - 1;
                    var dropdownToRemove = newRaceDropdowns[indexLast];

                    Destroy(dropdownToRemove);
                    newRaceDropdowns.RemoveAt(indexLast);
                }
            }
        }

        public void StartGame()
        {
            for (int i = 0; i < newRaceDropdowns.Count; i++)
            {
                var dropdown = newRaceDropdowns[i].GetComponent<Dropdown>();
                var playerRace = (RaceEnum)Enum.Parse(typeof(RaceEnum), dropdown.options[dropdown.value].text);
                var newPlayer = new PlayerModel();

                newPlayer.Nickname = $"Player_{i + 1}";
                newPlayer.Race = playerRace;

                gameInfoStorageController.Players.Add(newPlayer);
            }

            SceneManager.LoadScene(GameScene);
        }
    }
}

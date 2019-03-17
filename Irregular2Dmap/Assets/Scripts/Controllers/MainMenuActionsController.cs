using Assets.Scripts.Enums;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MainMenuActionsController : MonoBehaviour
    {
        public GameObject GameInfoStorageObject;

        private GameInfoStorageController gameInfoStorageController;

        void Awake()
        {
            gameInfoStorageController = GameInfoStorageObject.GetComponent<GameInfoStorageController>();
        }

        public void PickMap(Dropdown mapsDropdown)
        {
            gameInfoStorageController.MapName = mapsDropdown.options[mapsDropdown.value].text;
        }

        public void PickRace(Dropdown racesDropdown)
        {
            gameInfoStorageController.Race = (RaceEnum) Enum.Parse(typeof(RaceEnum), racesDropdown.options[racesDropdown.value].text);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

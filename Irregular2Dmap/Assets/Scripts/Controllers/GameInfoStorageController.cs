using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameInfoStorageController : MonoBehaviour
    {
        public static GameInfoStorageController Instance = null;

        public RaceEnum Race;
        public string MapName;

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
        }
    }
}

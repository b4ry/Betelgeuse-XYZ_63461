﻿using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameInfoStorageController : MonoBehaviour
    {
        public static GameInfoStorageController Instance = null;

        public string MapName;
        public int PlayersNumber = 1;
        public List<PlayerModel> Players;

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

            Players = new List<PlayerModel>();
        }
    }
}

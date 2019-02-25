using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Readers
{
    public class SpritesReader : MonoBehaviour
    {
        //public static SpritesReader Instance = null;

        public List<Sprite> BiomeImageSprites = new List<Sprite>();
        public List<Sprite> ResourceImageSprites = new List<Sprite>();
        public List<Sprite> RarityImageSprites = new List<Sprite>();
        public List<Sprite> OddityImageSprites = new List<Sprite>();

        void Awake()
        {
            //if (Instance == null)
            //{
            //    Instance = this;
            //}
            //else if (Instance != this)
            //{
            //    Destroy(gameObject);
            //}

            //DontDestroyOnLoad(gameObject);

            //TODO: MOVE TO ASSET BUNDLES
            BiomeImageSprites = Resources.LoadAll<Sprite>("UI/WorldMapUICanvas/RegionSummaryPanel/BiomeSprites").ToList();
            ResourceImageSprites = Resources.LoadAll<Sprite>("UI/WorldMapUICanvas/RegionSummaryPanel/ResourceSprites").ToList();
            RarityImageSprites = Resources.LoadAll<Sprite>("UI/WorldMapUICanvas/RegionSummaryPanel/RaritySprites").ToList();
            OddityImageSprites = Resources.LoadAll<Sprite>("UI/WorldMapUICanvas/RegionSummaryPanel/OdditySprites").ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class WeightRandomSpriteEx : TileExtension
    {
        [Serializable]
        public struct WeightedSprite
        {
#if ODIN_INSPECTOR
            [HideLabel]
#endif
            public int Weight;
#if ODIN_INSPECTOR
            [HideLabel, PreviewField(ObjectFieldAlignment.Left)]
 #endif
            public Sprite Sprite;
        }

#if ODIN_INSPECTOR
        [ListDrawerSettings(DraggableItems = false)]
  #endif
        [SerializeField] public List<WeightedSprite> Sprites = new List<WeightedSprite>();

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            if (Sprites == null || Sprites.Count <= 0) return tileData;

            var oldState = Random.state;
            long hash = position.x;
            hash = hash + 0xabcd1234 + (hash << 15);
            hash = hash + 0x0987efab ^ (hash >> 11);
            hash ^= position.y;
            hash = hash + 0x46ac12fd + (hash << 7);
            hash = hash + 0xbe9730af ^ (hash << 11);
            Random.InitState((int)hash);

            // Get the cumulative weight of the sprites
            var cumulativeWeight = 0;
            foreach (var spriteInfo in Sprites) cumulativeWeight += spriteInfo.Weight;

            // Pick a random weight and choose a sprite depending on it
            var randomWeight = Random.Range(0, cumulativeWeight);


            foreach (var spriteInfo in Sprites)
            {
                randomWeight -= spriteInfo.Weight;

                if (randomWeight < 0)
                {
                    tileData.sprite = spriteInfo.Sprite;

                    break;
                }
            }

            Random.state = oldState;

            return tileData;
        }
    }
}

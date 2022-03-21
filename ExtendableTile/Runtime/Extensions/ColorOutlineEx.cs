using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class ColorOutlineEx : TileExtension
    {
        [SerializeField] private Perlin _perlin = default;
        [SerializeField] private ParticleSystem.MinMaxGradient _colorGradient = new ParticleSystem.MinMaxGradient {color = Color.white, mode = ParticleSystemGradientMode.Color};
        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            float check = 0;

            void Action(TileBase tile, Vector3Int pos)
            {
                if (!tile)
                {
                    check++;
                }
            }

            tilemap.ForEach(position, Action);
            tileData.color *= _colorGradient.Evaluate(_perlin.Evaluate(position.x, position.y, check / 8f));

            return tileData;
        }
    }
}

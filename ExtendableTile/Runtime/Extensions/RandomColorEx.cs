using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class RandomColorEx : TileExtension
    {
        [SerializeField] private Perlin _perlin= default;
        [SerializeField] private ParticleSystem.MinMaxGradient _colorGradient = new ParticleSystem.MinMaxGradient
        {
            color = Color.white, mode = ParticleSystemGradientMode.Color
        };
        
        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            tileData.color *= _colorGradient.Evaluate(_perlin.Evaluate(position.x, position.y, Random.value));

            return tileData;
        }
    }
}

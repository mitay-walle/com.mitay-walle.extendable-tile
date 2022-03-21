using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class ScaleMapEx : TileExtension
    {
        [SerializeField] private Perlin _perlin = default;


        [SerializeField] private ParticleSystem.MinMaxCurve _curve = new ParticleSystem.MinMaxCurve() {constant = 1};
        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            var s = _curve.Evaluate(_perlin.Evaluate(position.x, position.y, Random.value));

            var tr = tileData.transform;
            tr.m00 = s;
            tr.m11 = s;
            tileData.flags |= TileFlags.LockTransform;
            tileData.transform = tr;

            return tileData;
        }
    }
}

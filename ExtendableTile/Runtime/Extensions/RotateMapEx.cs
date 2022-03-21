using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class RotateMapEx : TileExtension
    {
        [SerializeField] private ParticleSystem.MinMaxCurve _curve = new ParticleSystem.MinMaxCurve() {constant = 1};
        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            float r = _curve.Evaluate(Random.value);
            var tr = tileData.transform;
            tr.SetTRS(tr.GetColumn(3), Quaternion.Euler(0, 0, r), tr.lossyScale);
            tileData.flags |= TileFlags.LockTransform;
            tileData.transform = tr;

            return tileData;
        }
    }
}

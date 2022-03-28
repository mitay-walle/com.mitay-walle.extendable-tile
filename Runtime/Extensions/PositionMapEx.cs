using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class PositionMapEx : TileExtension
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField] private ParticleSystem.MinMaxCurve _curveX = new ParticleSystem.MinMaxCurve() {constant = 0};
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField] private ParticleSystem.MinMaxCurve _curveY = new ParticleSystem.MinMaxCurve() {constant = 0};
        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            float x = _curveX.Evaluate(Random.value);
            float y = _curveY.Evaluate(Random.value);
            var tr = tileData.transform;
            Vector3 pos = tr.GetColumn(3);
            pos.x += x;
            pos.y += y;
            tr.SetTRS(pos, tr.rotation, tr.lossyScale);
            tileData.flags |= TileFlags.LockTransform;
            tileData.transform = tr;

            return tileData;
        }
    }
}

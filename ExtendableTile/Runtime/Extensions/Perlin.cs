using System;
using UnityEngine;
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class Perlin
    {
        [SerializeField] private bool usePerlin = default;
#if ODIN_INSPECTOR
    [ShowIf(nameof(usePerlin))]
#endif
        [SerializeField] private Vector2 _perlinScale = Vector2.one;
#if ODIN_INSPECTOR
    [ShowIf(nameof(usePerlin))]
#endif
        [SerializeField] private Vector2 _perlinOffset = Vector2.zero;

        public float Evaluate(float x, float y, float defaultValue)
        {
            if (!usePerlin) return defaultValue;
            return Mathf.PerlinNoise((_perlinOffset.x + x) * _perlinScale.x, (_perlinOffset.y + y) * _perlinScale.y);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Tilemaps;
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

namespace Plugins.Extendable.Runtime.Extensions
{
    [CreateAssetMenu(menuName = "2D/Tiles/Extension")]
    public class TileExtensionSO : ScriptableObject
    {
#if ODIN_INSPECTOR
    [HideLabel]
#endif
        [SerializeReference] public TileExtension Data;
    }

    [Serializable]
    public class ScriptableObjectEx : TileExtension
    {
#if ODIN_INSPECTOR
    [InlineEditor, HideLabel]
#endif
        [SerializeField] private TileExtensionSO _data= default;

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            _data.Data.RefreshTile(position, tilemap);
        }

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return _data.Data.StartUp(position, tilemap, go);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            if (!_isEnabled) return false;

            return _data.Data.GetTileAnimationData(position, tilemap, ref tileAnimationData);
        }

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            return _data.Data.GetTileData(position, tilemap, ref tileData);
        }
    }
}

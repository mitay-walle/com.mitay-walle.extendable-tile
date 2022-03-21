using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.Extendable.Runtime
{
    [Serializable]
    public abstract class TileExtension
    {
        [SerializeField] protected bool _isEnabled = true;
        
        public virtual void RefreshTile(Vector3Int position, ITilemap tilemap) { }
        public virtual bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go) { return false; }
        public abstract TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData);
        public virtual bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData animationData)
        {
            animationData = default;
            return false;
        }
    }
}

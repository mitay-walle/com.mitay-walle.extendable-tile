using System;
using Plugins.Extendable.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.mitaywalle.ExtendableTile.Runtime.Extensions
{
    /// <summary>
    /// Uses other existing TileBase to execute it's behaviour
    /// </summary>
    [Serializable]
    public class NestedTileEx : TileExtension
    {
        [SerializeField] private TileBase _tile;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return _isEnabled && _tile && _tile.StartUp(position, tilemap, go);
        }

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            if (_isEnabled && _tile) _tile.RefreshTile(position, tilemap);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData animationData)
        {
            return _isEnabled && _tile && _tile.GetTileAnimationData(position, tilemap, ref animationData);
        }

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (_isEnabled && _tile) _tile.GetTileData(position, tilemap, ref tileData);

            return tileData;
        }
    }
}

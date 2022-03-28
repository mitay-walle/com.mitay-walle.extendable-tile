using System;
using Plugins.mitaywalle.ExtendableTile.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class PipelineTileEx : TileExtension
    {
        [SerializeField] private TileBase _myTile = default;
        [LabeledArray(new[] {"* Dot", "┐ Left Bot Angle", " | Vertical Line", "┼ Cross", "┴ T Cross"})]
        [SerializeField] private Sprite[] _sprites = new Sprite[5];

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            for (int yd = -1; yd <= 1; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int pos = new Vector3Int(position.x + xd, position.y + yd, position.z);

                if (TileValue(tilemap, pos))
                    tilemap.RefreshTile(pos);
            }
        }

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!_isEnabled) return tileData;

            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;

            int mask = TileValue(tilemap, position + new Vector3Int(0, 1, 0)) ? 1 : 0;
            mask += TileValue(tilemap, position + new Vector3Int(1, 0, 0)) ? 2 : 0;
            mask += TileValue(tilemap, position + new Vector3Int(0, -1, 0)) ? 4 : 0;
            mask += TileValue(tilemap, position + new Vector3Int(-1, 0, 0)) ? 8 : 0;

            int index = GetIndex((byte)mask);

            if (index >= 0 && index < _sprites.Length && TileValue(tilemap, position))
            {
                tileData.sprite = _sprites[index];
                tileData.transform = GetTransform((byte)mask);
                tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
                tileData.colliderType = Tile.ColliderType.Sprite;
            }

            return tileData;
        }

        private bool TileValue(ITilemap tileMap, Vector3Int position)
        {
            TileBase tile = tileMap.GetTile(position);

            return (tile != null && tile == _myTile);
        }

        private int GetIndex(byte mask)
        {
            switch (mask)
            {
                case 0: return 0;

                case 3:
                case 6:
                case 9:
                case 12: return 1;

                case 1:
                case 2:
                case 4:
                case 5:
                case 10:
                case 8: return 2;

                case 15: return 3;

                case 7:
                case 11:
                case 13:
                case 14: return 4;
            }

            return -1;
        }

        private Matrix4x4 GetTransform(byte mask)
        {
  
            switch (mask)
            {
                case 9:
                case 10:
                case 7:
                case 2:
                case 8:
                    return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -90), Vector3.one);

                case 3:
                case 14:
                    return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -180), Vector3.one);

                case 6:
                case 13:
                    return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -270), Vector3.one);
            }

            return Matrix4x4.identity;
        }
    }
}

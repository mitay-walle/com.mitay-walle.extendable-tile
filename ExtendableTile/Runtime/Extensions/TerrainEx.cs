using System;
using Plugins.mitaywalle.ExtendableTile.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.Extendable.Runtime
{
    public enum eTerrainSpriteID
    {
        _filled = 0,
        _3SidesLeftBotRight = 1,
        _2Sides1Corner = 2,
        _2AdjSides = 3,
        _2OppSides = 4,
        _1Side2Corners = 5,
        _1Side1CornerLow = 6,
        _1Side1CornerUp = 7,
        _1Side = 8,
        _4Corners = 9,
        _3Corners = 10,
        _2AdjCorners = 11,
        _2OppCorners = 12,
        _1Corner = 13,
        empty = 14,
        invalid = -1,
    }

    public static class Directions
    {
        public static readonly Vector3Int UP = Vector3Int.up;
        public static readonly Vector3Int DOWN = Vector3Int.down;
        public static readonly Vector3Int LEFT = Vector3Int.left;
        public static readonly Vector3Int RIGHT = Vector3Int.right;
        public static readonly Vector3Int UP_RIGHT = UP + RIGHT;
        public static readonly Vector3Int UP_LEFT = UP + LEFT;
        public static readonly Vector3Int DOWN_RIGHT = DOWN + RIGHT;
        public static readonly Vector3Int DOWN_LEFT = DOWN + LEFT;
    }

    public static class Matricies
    {
        public static readonly Matrix4x4 UP = Matrix4x4.identity;
        public static readonly Matrix4x4 DOWN = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,180), Vector3.one);
        public static readonly Matrix4x4 LEFT = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,-90), Vector3.one);
        public static readonly Matrix4x4 RIGHT = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,90), Vector3.one);
        // public static readonly Vector3Int UP_RIGHT = UP + RIGHT;
        // public static readonly Vector3Int UP_LEFT = UP + LEFT;
        // public static readonly Vector3Int DOWN_RIGHT = DOWN + RIGHT;
        // public static readonly Vector3Int DOWN_LEFT = DOWN + LEFT;
    }

    
    [Serializable]
    public class TerrainEx : TileExtension,ISerializationCallbackReceiver
    {
        private const int TOTAL_TILES = 15;
        [SerializeField] private TileBase _myTile = default;

        [SerializeField, Range(-1, 255)] private int _testedMaskValue = -1;
        [SerializeField] private eTerrainSpriteID _testedMaskType = eTerrainSpriteID.invalid;
        #if ODIN_INSPECTOR
        #else
        [LabeledArray(typeof(eTerrainSpriteID))]
        #endif
        [SerializeField] private Sprite[] _sprites = new Sprite[15];

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            for (int yd = -1; yd <= 1; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int pos = new Vector3Int(position.x + xd, position.y + yd, position.z);

                if (IsSameTile(tilemap, pos))
                    tilemap.RefreshTile(pos);
            }
        }

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;

            int mask = IsSameTile(tilemap, position + Directions.UP) ? 1 : 0;
            mask += IsSameTile(tilemap, position + Directions.UP_RIGHT) ? 2 : 0;
            mask += IsSameTile(tilemap, position + Directions.RIGHT) ? 4 : 0;
            mask += IsSameTile(tilemap, position + Directions.DOWN_RIGHT) ? 8 : 0;
            mask += IsSameTile(tilemap, position + Directions.DOWN) ? 16 : 0;
            mask += IsSameTile(tilemap, position + Directions.DOWN_LEFT) ? 32 : 0;
            mask += IsSameTile(tilemap, position + Directions.LEFT) ? 64 : 0;
            mask += IsSameTile(tilemap, position + Directions.UP_LEFT) ? 128 : 0;

            byte original = (byte)mask;

            if ((original | 254) < 255) { mask &= 125; }

            if ((original | 251) < 255) { mask &= 245; }

            if ((original | 239) < 255) { mask &= 215; }

            if ((original | 191) < 255) { mask &= 95; }

            int index = (int)GetSpriteID(mask);

            if (index >= 0 && index < _sprites.Length && IsSameTile(tilemap, position))
            {
                tileData.sprite = _sprites[index];
                tileData.transform = GetTransform((byte)mask);
                tileData.flags |= TileFlags.LockTransform;
            }

            return tileData;
        }


        private bool IsSameTile(ITilemap tileMap, Vector3Int position)
        {
            TileBase tile = tileMap.GetTile(position);

            return (tile != null && tile == _myTile);
        }


        private eTerrainSpriteID GetSpriteID(int mask)
        {
            switch (mask)
            {
                case 0: return eTerrainSpriteID._filled;

                case 1:
                case 4:
                case 16:
                case 64: return eTerrainSpriteID._3SidesLeftBotRight;

                case 5:
                case 20:
                case 80:
                case 65: return eTerrainSpriteID._2Sides1Corner;

                case 7:
                case 28:
                case 112:
                case 193: return eTerrainSpriteID._2AdjSides;

                case 17:
                case 68: return eTerrainSpriteID._2OppSides;

                case 21:
                case 84:
                case 81:
                case 69: return eTerrainSpriteID._1Side2Corners;

                case 23:
                case 92:
                case 113:
                case 197: return eTerrainSpriteID._1Side1CornerLow;

                case 29:
                case 116:
                case 209:
                case 71: return eTerrainSpriteID._1Side1CornerUp;

                case 31:
                case 124:
                case 241:
                case 199: return eTerrainSpriteID._1Side;

                case 85: return eTerrainSpriteID._4Corners;

                case 87:
                case 93:
                case 117:
                case 213: return eTerrainSpriteID._3Corners;

                case 95:
                case 125:
                case 245:
                case 215: return eTerrainSpriteID._2AdjCorners;

                case 119:
                case 221: return eTerrainSpriteID._2OppCorners;

                case 127:
                case 253:
                case 247:
                case 223: return eTerrainSpriteID._1Corner;

                case 255: return eTerrainSpriteID.empty;
            }

            return eTerrainSpriteID.invalid;
        }

        private Matrix4x4 GetTransform(byte mask)
        {
            if (_testedMaskValue == mask)
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -45), Vector3.one);
            }

            switch (mask)
            {
                case 87:
                case 247:
                case 65:
                case 193:
                case 69:
                case 197:
                case 71:
                case 199:
                case 215:
                case 4:
                case 68:
                {
                    return Matricies.DOWN;
                }

                case 221:
                case 29:
                case 23:
                case 7:
                case 31:
                case 95:
                case 21:
                case 223:
                case 5:
                case 16:
                case 17:
                case 93:
                {
                    return Matricies.RIGHT;

                }

                case 1:
                case 80:
                case 112:
                case 81:
                case 113:
                case 209:
                case 213:
                case 241:
                case 245:
                case 253:
                {
                    return Matricies.LEFT;

                }
            }

            return Matricies.UP;
        }
        public void OnBeforeSerialize()
        {
            _testedMaskType = GetSpriteID(_testedMaskValue);
        }
        public void OnAfterDeserialize()
        {
            
        }
    }
}

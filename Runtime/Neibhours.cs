using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.Extendable.Runtime
{
    public static class Neibhours
    {
        public static void ForEach(this ITilemap tilemap, Vector3Int center, Action<TileBase, Vector3Int> action)
        {
            void Action(Vector3Int pos)
            {
                var tile = tilemap.GetTile(pos);
                action.Invoke(tile, pos);
            }

            ForEach(center, Action);
        }

        public static void ForEach(Vector3Int center, Action<Vector3Int> action)
        {
            action.Invoke(new Vector3Int(center.x + 1, center.y, 0));
            action.Invoke(new Vector3Int(center.x + 1, center.y + 1, 0));
            action.Invoke(new Vector3Int(center.x + 1, center.y - 1, 0));

            action.Invoke(new Vector3Int(center.x, center.y, 0));
            action.Invoke(new Vector3Int(center.x, center.y + 1, 0));
            action.Invoke(new Vector3Int(center.x, center.y - 1, 0));

            action.Invoke(new Vector3Int(center.x - 1, center.y, 0));
            action.Invoke(new Vector3Int(center.x - 1, center.y + 1, 0));
            action.Invoke(new Vector3Int(center.x - 1, center.y - 1, 0));
        }
    }
}

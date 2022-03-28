using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Plugins.Extendable.Runtime
{
    [CreateAssetMenu(menuName = "2D/Tiles/Extendable Tile")]
    public class ExtendableTile : Tile
    {
#if !ODIN_INSPECTOR
    [TypeToLabel] 
#endif
        [SerializeReference] public List<TileExtension> Extensions = new List<TileExtension>();

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            void Action(TileExtension e) => e.RefreshTile(position, tilemap);
            ForEachExtension(Action);
        }
        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            bool check = base.StartUp(position, tilemap, go);

            void Action(TileExtension e)
            {
                if (e == null) return;
                check |= e.StartUp(position, tilemap, go);
            }

            ForEachExtension(Action);

            return check;
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            var data = tileData;

            void Action(TileExtension e)
            {
                if (e == null) return;
                data = e.GetTileData(position, tilemap, ref data);
            }

            ForEachExtension(Action);
            tileData = data;
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            bool check = base.GetTileAnimationData(position, tilemap, ref tileAnimationData);

            TileAnimationData data = tileAnimationData;

            void Action(TileExtension e)
            {
                if (e == null) return;
                check |= e.GetTileAnimationData(position, tilemap, ref data);
            }

            ForEachExtension(Action);

            if (check)
            {
                tileAnimationData = data;
            }

            return check;
        }

        private void ForEachExtension(Action<TileExtension> action) => Extensions.ForEach(action);
    }
}

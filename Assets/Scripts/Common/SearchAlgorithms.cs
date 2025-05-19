using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class SearchAlgorithms
{
    public void SearchMoveableArea(Tile[,] tile_map, int map_width, int map_height, Vector2Int start_grid_pos, int move_cost)
    {
        Dictionary<Vector2Int,TileNode> node_list = new Dictionary<Vector2Int, TileNode>();

        //Nodeデータを作成
        for (int y  = 0; y < tile_map.GetLength(1); y++)
        {
            for (int x = 0; x < tile_map.GetLength(0); x++)
            {
                node_list.Add(new Vector2Int(x, y), new TileNode(
                        NodeStateType.None,
                        0,
                        0,
                        0,
                        tile_map[x, y]
                    ));
            }
        }

        Debug.Log(node_list[new Vector2Int(1, 2)].tile.gameObject.transform.position);
    }

    //ノードの探索状態
    enum NodeStateType
    {
        None,
        Open,
        Close,
    }
    struct TileNode 
    {
        public NodeStateType type;
        public int c;   //実コスト
        public int h;   //推定コスト
        public int s;   //スコア
        public Tile tile;

        public TileNode(NodeStateType type, int c, int h, int s, Tile tile)
        {
            this.type = type;
            this.c = c;
            this.h = h;
            this.s = s;
            this.tile = tile;
        }
    }
}

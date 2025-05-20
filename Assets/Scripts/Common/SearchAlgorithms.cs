using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEditorInternal.ReorderableList;
using System.IO;
public class SearchAlgorithms
{
    public void SearchMoveableArea(Tile[,] tile_map, int map_width, int map_height, Vector2Int start_grid_pos, int move_cost)
    {
        int n_step = 0;
        //範囲探索なので、推定コストは0
        //int default_h = 0;
        //探索するグリッド座標
        Vector2Int target_grid_pos = start_grid_pos;
        Dictionary<Vector2Int, TileNode> node_dict = CreateTileNode(tile_map);

        //最初のノードを探索
        OpenNode(new List<TileNode> { node_dict[target_grid_pos] }, n_step);

        List<TileNode> around_node = new List<TileNode>();
        Vector2Int[] around_grid_pos = new Vector2Int[4]{
            new Vector2Int(target_grid_pos.x, target_grid_pos.y + 1),
            new Vector2Int(target_grid_pos.x, target_grid_pos.y - 1),
            new Vector2Int(target_grid_pos.x + 1, target_grid_pos.y),
            new Vector2Int(target_grid_pos.x - 1, target_grid_pos.y)
        };

        //周囲4マスのタイルを探索
        foreach(Vector2Int grid_pos in around_grid_pos)
        {
            //グリッド座標にタイルがある場合
            if (node_dict.ContainsKey(grid_pos))
            {
                around_node.Add(node_dict[grid_pos]);
            }
        }

        foreach(TileNode node in around_node)
        {
            Debug.Log($"周囲タイルの座標{node.tile.gameObject.transform.position}");
        }
    }

    /// <summary>
    /// ノードをOpenにする
    /// </summary>
    /// <param name="node_list">
    /// Openにするノードリスト
    /// </param>
    private void OpenNode(List<TileNode> node_list, int n_step)
    {
        int default_h = 0;

        int c;
        foreach (TileNode node in node_list)
        {
            //探索開始地点
            if (n_step == 0)
            {
                c = 0;
            }
            else
            {
                c = node.c;
            }
            //実コスト
            node.c = c;
            //推定コスト
            node.h = default_h;
            //スコア
            node.s = node.c + node.h;
            node.node_state = NodeStateType.Open;
        }
    }

    /// <summary>
    /// タイルのノード辞書を作成
    /// </summary>
    /// <param name="tile_map">
    /// タイルの配置
    /// </param>
    /// <returns>
    /// キー: グリッド座標、 バリュー: Node
    /// </returns>
    private Dictionary<Vector2Int, TileNode> CreateTileNode(Tile[,] tile_map)
    {
        Dictionary<Vector2Int, TileNode> node_list = new Dictionary<Vector2Int, TileNode>();

        //Nodeデータを作成
        for (int y = 0; y < tile_map.GetLength(1); y++)
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

        return node_list;
    }

    //ノードの探索状態
    enum NodeStateType
    {
        None,
        Open,
        Close,
    }
    class TileNode 
    {
        public NodeStateType node_state;
        public int c;   //実コスト
        public int h;   //推定コスト
        public int s;   //スコア
        public Tile tile;

        public TileNode(NodeStateType node_state, int c, int h, int s, Tile tile)
        {
            this.node_state = node_state;
            this.c = c;
            this.h = h;
            this.s = s;
            this.tile = tile;
        }
    }
}

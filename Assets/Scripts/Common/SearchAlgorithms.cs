using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchAlgorithms
{
    private Dictionary<Vector2Int, GridNode> node_dict = new Dictionary<Vector2Int, GridNode>();
    private List<GridNode> searched_nodes = new List<GridNode>();

    public List<GridNode> GetSearchedNodes()
    {
        return searched_nodes;
    }

    public List<Vector2Int> SearchMoveableArea(Tile[,] tile_map, int map_width, int map_height, Vector2Int start_grid_pos, int movable_area)
    {
        node_dict.Clear();
        searched_nodes.Clear();
        node_dict = CreateTileNode(tile_map);

        Queue<Vector2Int> open_queue = new Queue<Vector2Int>();
        node_dict[start_grid_pos].cost_from_start = 0;
        node_dict[start_grid_pos].is_searched = true;
        open_queue.Enqueue(start_grid_pos);

        while (open_queue.Count > 0)
        {
            //キューからポップ
            Vector2Int current_pos = open_queue.Dequeue();
            GridNode current_node = node_dict[current_pos];

            //ノードのコストが移動可能範囲を超えた場合、処理をしない
            if (current_node.cost_from_start >= movable_area)
                continue;

            //再探索点の場合追加しない
            if (!searched_nodes.Contains(current_node))
            {
                searched_nodes.Add(current_node);
            }

            foreach (Vector2Int neighbor_pos in GetNeighbors(current_pos))
            {
                //範囲外の場合処理しない
                if (!node_dict.ContainsKey(neighbor_pos)) continue;

                GridNode neighbor_node = node_dict[neighbor_pos];
                //中心点までの移動コスト + 自分の移動コスト
                int tentative_cost = current_node.cost_from_start + neighbor_node.tile.move_cost;

                //未探索、または短い経路になる場合
                if (!neighbor_node.is_searched || tentative_cost < neighbor_node.cost_from_start)
                {
                    Debug.Log($"{neighbor_pos}のコスト: {tentative_cost}");
                    neighbor_node.cost_from_start = tentative_cost;
                    neighbor_node.is_searched = true;
                    open_queue.Enqueue(neighbor_pos);
                }
            }
        }

        return searched_nodes
            .Where(node => node.cost_from_start <= movable_area)
            .Select(node => node.grid_pos)
            .ToList();
    }

    private Dictionary<Vector2Int, GridNode> CreateTileNode(Tile[,] tile_map)
    {
        Dictionary<Vector2Int, GridNode> node_list = new Dictionary<Vector2Int, GridNode>();
        int width = tile_map.GetLength(0);
        int height = tile_map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                node_list[pos] = new GridNode(pos, tile_map[x, y]);
            }
        }

        return node_list;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        return new List<Vector2Int>
        {
            new Vector2Int(pos.x, pos.y + 1),
            new Vector2Int(pos.x, pos.y - 1),
            new Vector2Int(pos.x + 1, pos.y),
            new Vector2Int(pos.x - 1, pos.y)
        };
    }

    public class GridNode
    {
        public bool is_searched = false;
        public int cost_from_start = int.MaxValue;
        public Tile tile;
        public Vector2Int grid_pos;

        public GridNode(Vector2Int grid_pos, Tile tile)
        {
            this.grid_pos = grid_pos;
            this.tile = tile;
        }
    }
}

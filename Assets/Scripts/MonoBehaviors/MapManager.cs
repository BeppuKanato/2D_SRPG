using UnityEngine;
using System.Collections.Generic;
using System;

public class MapManager : MonoBehaviour
{
    [SerializeField] float sprite_width = 1f;
    [SerializeField] float sprite_height = 1f;
    [SerializeField] MapInfo map_info;
    [SerializeField] FactoryWithIntKey tile_factory;
    [SerializeField] Transform field_parent;

    CSVHandler csv_handler;
    public Tile[,] map_tiles { get; private set; }
    int[,] map_int_data;

    [SerializeField] bool is_debug_mode = false;
    [Tooltip("変更先のタイル"), SerializeField] TileType change_type;

    void Awake()
    {
        if (map_info == null || tile_factory == null)
        {
            Debug.LogError("必要なコンポーネントがInspectorで設定されていません！");
        }
        csv_handler = new CSVHandler();
    }

    /// <summary>
    /// マップデータの読み込み、タイル生成を行う
    /// </summary>
    public void LoadAndGenerateMap()
    {
        map_int_data = ReadMapData();
        MapDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) is_debug_mode = !is_debug_mode;

        if (is_debug_mode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2Int gridPos = WorldToGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (IsInBounds(gridPos))
                {
                    Tile clickedTile = GetTileData(gridPos);
                    Debug.Log($"クリックした位置: {gridPos}, タイル種別: {clickedTile.tile_type}");
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector2Int gridPos = WorldToGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (IsInBounds(gridPos))
                {
                    Dictionary<Vector2Int, int> update = new Dictionary<Vector2Int, int>
                    {
                        { gridPos, (int)TileType.GRASS }
                    };
                    UpdateMapData(update);
                }
            }
        }
    }

    /// <summary>
    /// CSVからマップデータを読み込む
    /// </summary>
    /// <returns>
    /// csvの数値データ
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int[,] ReadMapData()
    {
        CSVHandler.ReadResult read_result = csv_handler.ReadCSV($"MapData/{map_info.csv_file_name}");
        if (!read_result.is_success)
        {
            throw new InvalidOperationException($"CSV読み込み失敗: {read_result.error_message}");
        }

        if (read_result.read_data.Count != map_info.height || read_result.read_data[0].Length != map_info.width)
        {
            Debug.Log($"マップサイズ不一致: mapInfo: height={map_info.height} width={map_info.width}, CSV: height={read_result.read_data.Count} width={read_result.read_data[0].Length}");
        }

        int[,] result = new int[map_info.width, map_info.height];
        for (int y = 0; y < map_info.height; y++)
        {
            for (int x = 0; x < map_info.width; x++)
            {
                result[x, y] = int.Parse(read_result.read_data[y][x]);
            }
        }

        return result;
    }

    /// <summary>
    /// 指定グリッド座標のタイルを返す
    /// </summary>
    /// <param name="pos">
    /// グリッド座標
    /// </param>
    /// <returns>
    /// タイルデータ
    /// </returns>
    public Tile GetTileData(Vector2Int pos)
    {
        return IsInBounds(pos) ? map_tiles[pos.x, pos.y] : null;
    }

    /// <summary>
    /// 指定座標のマップデータを変更する
    /// </summary>
    /// <param name="update_data">
    /// 更新するタイルの座標、更新後のタイル
    /// </param>
    public void UpdateMapData(Dictionary<Vector2Int, int> update_data)
    {
        foreach (var key_value in update_data)
        {
            Vector2Int pos = key_value.Key;
            if (!IsInBounds(pos)) continue;

            TileType tile_type = (TileType)key_value.Value;
            Vector2 generate_pos = map_tiles[pos.x, pos.y].transform.position;
            Destroy(map_tiles[pos.x, pos.y].gameObject);

            Tile new_tile = tile_factory.InstantiateFromIntKey(field_parent, (int)tile_type, generate_pos, Quaternion.identity).GetComponent<Tile>();
            map_tiles[pos.x, pos.y] = new_tile;
        }
    }

    /// <summary>
    /// 引数がマップ範囲内かを返す
    /// </summary>
    /// <param name="pos">
    /// 調べる座標
    /// </param>
    /// <returns>
    /// マップ内: true, マップ外: false
    /// </returns>
    public bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < map_info.width && pos.y >= 0 && pos.y < map_info.height;
    }

    /// <summary>
    /// マップを描画する
    /// </summary>
    public void MapDisplay()
    {
        map_tiles = new Tile[map_info.width, map_info.height];
        for (int y = 0; y < map_info.height; y++)
        {
            for (int x = 0; x < map_info.width; x++)
            {
                Vector2 pos = new Vector2(x * sprite_width, -y * sprite_height);
                map_tiles[x, y] = tile_factory.InstantiateFromIntKey(field_parent, map_int_data[x, y], pos, Quaternion.identity).GetComponent<Tile>();
            }
        }
    }

    /// <summary>
    /// マップを描画し直す
    /// </summary>
    public void RefreshMapDisplay()
    {
        foreach (Tile tile in map_tiles)
        {
            Destroy(tile.gameObject);
        }
        MapDisplay();
    }

    Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / sprite_width);
        int y = Mathf.FloorToInt(-worldPos.y / sprite_height); // 上方向がy正ならこれで正しい
        return new Vector2Int(x, y);
    }
}

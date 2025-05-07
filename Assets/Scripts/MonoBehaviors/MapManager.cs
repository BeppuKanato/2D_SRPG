using UnityEngine;
using System.Collections.Generic;
using System;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    float sprite_width = 1f;
    [SerializeField]
    float sprite_height = 1f;
    [SerializeField]
    MapInfo map_info;
    [SerializeField]
    TileFactory tile_factory;

    CSVHandler csv_handler;
    Tile[,] map_tiles;
    int[,] map_int_data;

    //デバッグ用変数
    [SerializeField]
    bool is_debug_mode = false;
    [Tooltip("変更先のタイル"), SerializeField]
    TileType change_type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (map_info == null || tile_factory == null)
        {
            Debug.LogError("必要なコンポーネントがInspectorで設定されていません！");
        }
        csv_handler = new CSVHandler();
        map_int_data = ReadMapData();
        MapDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            is_debug_mode = !is_debug_mode;
        } 
        if (is_debug_mode)
        {
            if (Input.GetMouseButtonDown(0)) // 左クリック
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPos = WorldToGridPosition(worldPos);

                if (IsInBounds(gridPos))
                {
                    Tile clickedTile = GetTileData(gridPos);
                    Debug.Log($"クリックした位置: {gridPos}, タイル種別: {clickedTile.tile_type}");
                }
            }

            if (Input.GetMouseButtonDown(1)) // 右クリック
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPos = WorldToGridPosition(worldPos);

                if (IsInBounds(gridPos))
                {
                    Dictionary<Vector2Int, int> update = new Dictionary<Vector2Int, int>
                {
                    { gridPos, (int)TileType.GRASS } // 任意の種類に変更
                };
                    UpdateMapData(update);
                }
            }
        }
    }
    /// <summary>
    /// マップデータを取得
    /// </summary>
    /// <returns>string[,] マップの数値データ</returns>
    /// <exception cref="System.Exception">マップデータに問題がある場合例外を出す</exception>
    public int[,] ReadMapData()
    {
        CSVHandler.ReadResult read_result = csv_handler.ReadCSV($"MapData/{map_info.csv_file_name}");

        if (!read_result.is_success)
        {
            Debug.Log($"マップデータ読み込みに失敗しました\n error: {read_result.error_message}");

            throw new InvalidOperationException($"CSV読み込み失敗: {read_result.error_message}");
        }

        if (read_result.read_data.Count != map_info.height || read_result.read_data[0].Length != map_info.width)
        {
            Debug.Log($"マップデータとマップ情報間でマップサイズが異なっています\n " +
                $"mapInfo: height = {map_info.height} width = {map_info.width}, read_data: height = {read_result.read_data.Count} width = {read_result.read_data[0].Length}");
        }

        int[,] result = new int[read_result.read_data.Count, read_result.read_data[0].Length];
        for (int height = 0; height < read_result.read_data.Count; height++)
        {
            for (int width = 0; width < read_result.read_data[height].Length; width++)
            {
                //CSVで取得した文字列データを数値に変換
                result[height, width] = int.Parse(read_result.read_data[height][width]);
            }
        }

        return result;
    }
    /// <summary>
    /// 座標のタイルのデータを返す
    /// </summary>
    /// <param name="pos">
    /// タイルデータを取得する座標
    /// </param>
    /// <returns>Tileクラス</returns>
    public Tile GetTileData(Vector2Int pos)
    {
        return map_tiles[pos.y, pos.x];
    }
    /// <summary>
    /// マップ上の指定座標にあるタイルを、指定されたタイル種別に差し替えます。
    /// </summary>
    /// <param name="update_data">
    /// キー: 更新対象のタイル座標（Vector2Int）
    /// バリュー: 新しいタイルの種類（TileTypeにキャスト可能なint）
    /// </param>
    public void UpdateMapData(Dictionary<Vector2Int, int> update_data)
    {
        foreach(KeyValuePair<Vector2Int, int> key_value in update_data)
        {
            Vector2Int pos = key_value.Key;
            //範囲外の座標は処理しない
            if (!IsInBounds(pos))
            {
                continue;
            }

            TileType tile_type = (TileType)key_value.Value;

            //変更するタイルのローカル座標
            Vector2 generate_pos = map_tiles[pos.y, pos.x].gameObject.transform.position;
            Destroy(map_tiles[pos.y, pos.x].gameObject);

            Tile new_tile = tile_factory.InstantiateTile(tile_type, generate_pos, Quaternion.identity);
            map_tiles[pos.y, pos.x] = new_tile;
        }
    }
    /// <summary>
    /// マップ範囲内の座標かを返す
    /// </summary>
    /// <param name="pos">
    /// 判定する座標
    /// </param>
    /// <returns>範囲内: true, 範囲外: false</returns>
    public bool IsInBounds(Vector2Int pos)
    {
        if (pos.y >= map_info.height || pos.y < 0 
            || pos.x >= map_info.width || pos.x < 0)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    /// マップを描画
    /// </summary>
    public void MapDisplay()
    {
        Vector2 pos = Vector2.zero;
        map_tiles = new Tile[map_int_data.GetLength(0), map_int_data.GetLength(1)];

        for (int height = 0; height < map_int_data.GetLength(0); height++)
        {
            for (int width = 0; width < map_int_data.GetLength(1); width++)
            {
                map_tiles[height, width] = tile_factory.InstantiateTile((TileType)map_int_data[height, width], pos, Quaternion.identity);
                pos.x += sprite_width;
            }

            pos.y -= sprite_height;
            pos.x = 0;
        }
    }
    /// <summary>
    /// マップ描画をリフレッシュ
    /// </summary>
    public void RefreshMapDisplay()
    {
        foreach (Tile tile in map_tiles)
        {
            Destroy(tile.gameObject);
        }
        MapDisplay();
    }


    //デバッグ用
    /// <summary>
    /// ワールド座標をグリッド座標に変換
    /// </summary>
    /// <param name="worldPos">ワールド座標</param>
    /// <returns>グリッド座標</returns>
    Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        // Spriteの中心に合わせるための補正
        worldPos.x += sprite_width / 2f;
        worldPos.y -= sprite_height / 2f;

        int x = Mathf.FloorToInt(worldPos.x / sprite_width);
        int y = Mathf.FloorToInt(-worldPos.y / sprite_height); // 下が正方向なので反転
        return new Vector2Int(x, y);
    }

}

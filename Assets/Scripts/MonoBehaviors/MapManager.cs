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

    //�f�o�b�O�p�ϐ�
    [SerializeField]
    bool is_debug_mode = false;
    [Tooltip("�ύX��̃^�C��"), SerializeField]
    TileType change_type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (map_info == null || tile_factory == null)
        {
            Debug.LogError("�K�v�ȃR���|�[�l���g��Inspector�Őݒ肳��Ă��܂���I");
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
            if (Input.GetMouseButtonDown(0)) // ���N���b�N
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPos = WorldToGridPosition(worldPos);

                if (IsInBounds(gridPos))
                {
                    Tile clickedTile = GetTileData(gridPos);
                    Debug.Log($"�N���b�N�����ʒu: {gridPos}, �^�C�����: {clickedTile.tile_type}");
                }
            }

            if (Input.GetMouseButtonDown(1)) // �E�N���b�N
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPos = WorldToGridPosition(worldPos);

                if (IsInBounds(gridPos))
                {
                    Dictionary<Vector2Int, int> update = new Dictionary<Vector2Int, int>
                {
                    { gridPos, (int)TileType.GRASS } // �C�ӂ̎�ނɕύX
                };
                    UpdateMapData(update);
                }
            }
        }
    }
    /// <summary>
    /// �}�b�v�f�[�^���擾
    /// </summary>
    /// <returns>string[,] �}�b�v�̐��l�f�[�^</returns>
    /// <exception cref="System.Exception">�}�b�v�f�[�^�ɖ�肪����ꍇ��O���o��</exception>
    public int[,] ReadMapData()
    {
        CSVHandler.ReadResult read_result = csv_handler.ReadCSV($"MapData/{map_info.csv_file_name}");

        if (!read_result.is_success)
        {
            Debug.Log($"�}�b�v�f�[�^�ǂݍ��݂Ɏ��s���܂���\n error: {read_result.error_message}");

            throw new InvalidOperationException($"CSV�ǂݍ��ݎ��s: {read_result.error_message}");
        }

        if (read_result.read_data.Count != map_info.height || read_result.read_data[0].Length != map_info.width)
        {
            Debug.Log($"�}�b�v�f�[�^�ƃ}�b�v���ԂŃ}�b�v�T�C�Y���قȂ��Ă��܂�\n " +
                $"mapInfo: height = {map_info.height} width = {map_info.width}, read_data: height = {read_result.read_data.Count} width = {read_result.read_data[0].Length}");
        }

        int[,] result = new int[read_result.read_data.Count, read_result.read_data[0].Length];
        for (int height = 0; height < read_result.read_data.Count; height++)
        {
            for (int width = 0; width < read_result.read_data[height].Length; width++)
            {
                //CSV�Ŏ擾����������f�[�^�𐔒l�ɕϊ�
                result[height, width] = int.Parse(read_result.read_data[height][width]);
            }
        }

        return result;
    }
    /// <summary>
    /// ���W�̃^�C���̃f�[�^��Ԃ�
    /// </summary>
    /// <param name="pos">
    /// �^�C���f�[�^���擾������W
    /// </param>
    /// <returns>Tile�N���X</returns>
    public Tile GetTileData(Vector2Int pos)
    {
        return map_tiles[pos.y, pos.x];
    }
    /// <summary>
    /// �}�b�v��̎w����W�ɂ���^�C�����A�w�肳�ꂽ�^�C����ʂɍ����ւ��܂��B
    /// </summary>
    /// <param name="update_data">
    /// �L�[: �X�V�Ώۂ̃^�C�����W�iVector2Int�j
    /// �o�����[: �V�����^�C���̎�ށiTileType�ɃL���X�g�\��int�j
    /// </param>
    public void UpdateMapData(Dictionary<Vector2Int, int> update_data)
    {
        foreach(KeyValuePair<Vector2Int, int> key_value in update_data)
        {
            Vector2Int pos = key_value.Key;
            //�͈͊O�̍��W�͏������Ȃ�
            if (!IsInBounds(pos))
            {
                continue;
            }

            TileType tile_type = (TileType)key_value.Value;

            //�ύX����^�C���̃��[�J�����W
            Vector2 generate_pos = map_tiles[pos.y, pos.x].gameObject.transform.position;
            Destroy(map_tiles[pos.y, pos.x].gameObject);

            Tile new_tile = tile_factory.InstantiateTile(tile_type, generate_pos, Quaternion.identity);
            map_tiles[pos.y, pos.x] = new_tile;
        }
    }
    /// <summary>
    /// �}�b�v�͈͓��̍��W����Ԃ�
    /// </summary>
    /// <param name="pos">
    /// ���肷����W
    /// </param>
    /// <returns>�͈͓�: true, �͈͊O: false</returns>
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
    /// �}�b�v��`��
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
    /// �}�b�v�`������t���b�V��
    /// </summary>
    public void RefreshMapDisplay()
    {
        foreach (Tile tile in map_tiles)
        {
            Destroy(tile.gameObject);
        }
        MapDisplay();
    }


    //�f�o�b�O�p
    /// <summary>
    /// ���[���h���W���O���b�h���W�ɕϊ�
    /// </summary>
    /// <param name="worldPos">���[���h���W</param>
    /// <returns>�O���b�h���W</returns>
    Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        // Sprite�̒��S�ɍ��킹�邽�߂̕␳
        worldPos.x += sprite_width / 2f;
        worldPos.y -= sprite_height / 2f;

        int x = Mathf.FloorToInt(worldPos.x / sprite_width);
        int y = Mathf.FloorToInt(-worldPos.y / sprite_height); // �����������Ȃ̂Ŕ��]
        return new Vector2Int(x, y);
    }

}

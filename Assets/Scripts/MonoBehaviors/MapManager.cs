using UnityEngine;
using System.Collections.Generic;
using static CSVHandler;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    float sprite_width = 0.3f;
    [SerializeField]
    float sprite_height = 0.3f;
    [SerializeField]
    MapInfo map_info;
    [SerializeField]
    TileFactory tile_factory;

    CSVHandler csv_handler;
    Tile[,] map_tiles;
    int[,] map_int_data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (map_info == null || tile_factory == null)
        {
            Debug.LogError("�K�v�ȃR���|�[�l���g��Inspector�Őݒ肳��Ă��܂���I");
        }
        csv_handler = new CSVHandler();
        map_int_data = ReadMapData();
        InitializeMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �^�C���𐶐�
    /// </summary>
    public void InitializeMap()
    {
        Vector2 pos = Vector2.zero;
        map_tiles = new Tile[map_int_data.GetLength(0), map_int_data.GetLength(1)];

        for (int height = 0; height < map_int_data.GetLength(0); height++)
        {
            for (int width = 0; width < map_int_data.GetLength(1); width++)
            {
                pos.x += sprite_width;
                map_tiles[height, width] = tile_factory.InstantiateTile((TileType)map_int_data[height, width], pos, Quaternion.identity);
            }

            pos.y -= sprite_height;
            pos.x = 0;
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

            throw new System.Exception();
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
                Debug.Log($"{height}: {read_result.read_data[height][width]}");
                //CSV�Ŏ擾����������f�[�^�𐔒l�ɕϊ�
                result[height, width] = int.Parse(read_result.read_data[height][width]);
            }
        }

        return result;
    }
}

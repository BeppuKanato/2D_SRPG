using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class UnitManager : MonoBehaviour, ISaveToJson
{
    [SerializeField]
    MapManager map_manager;
    [SerializeField]
    FactoryWithStringKey ally_unit_factory;
    JsonHandler json_handler = new JsonHandler();

    public AllyUnitSaveData[] ally_unit_data;
    public List<AllyUnit> ally_units;


    //�}�b�v���(2025/5/18���_ ���ݒ�)
    public MapInfo map_info;

    /// <summary>
    /// ���j�b�g�f�[�^�����[�h�A��������
    /// </summary>
    public void LoadAndGenerateUnits()
    {
        //�S���j�b�g�̃f�[�^�����[�h
        LoadFromJson();
        ally_units = ally_unit_data.Select((data, index) => InstantiateAllyUnit(data, index)).ToList();
    }

    /// <summary>
    /// �������j�b�g�𐶐�����
    /// </summary>
    /// <param name="save_data">
    /// ���j�b�g�̃f�[�^
    /// </param>
    /// <returns>
    /// �����������j�b�g
    /// </returns>
    private AllyUnit InstantiateAllyUnit(AllyUnitSaveData save_data, int index)
    {
        Debug.Log(index);
        //�����ʒu�̃s�N�Z�����W���擾
        Vector2 pixel_pos = GetPixelPosition(map_info.initial_pos_ally[index]);
        
        GameObject unit = ally_unit_factory.InstantiateFromStringKey(null, save_data.unit_id, pixel_pos, Quaternion.identity);
        //���j�b�g�̃v���n�u���Ȃ������ꍇ
        if (unit == null)
        {
            return null;
        }
        AllyUnit ally_unit = unit.GetComponent<AllyUnit>();
        ally_unit.SetStatus(save_data);
        ally_unit.SetGridPos(map_info.initial_pos_ally[index]);

        return ally_unit;
    }

    /// <summary>
    /// �^�C���̃s�N�Z�����W��Ԃ�
    /// </summary>
    /// <param name="grid_pos">
    /// �s�N�Z�����W���擾�������^�C���̃O���b�h���W
    /// </param>
    /// <returns>
    /// �s�N�Z�����W(Vector2)
    /// </returns>
    private Vector2 GetPixelPosition(Vector2Int grid_pos)
    {
        return map_manager.GetTileData(grid_pos).gameObject.transform.position;
    }

    public void SaveToJson()
    {
        //string file_path = "Status/Unit/ally_unit_status";
        //JsonHandler.WriteResult result = json_handler.WriteJson();
    }

    /// <summary>
    /// - �������j�b�g�̃X�e�[�^�X
    /// 
    /// ���擾
    /// </summary>
    public void LoadFromJson()
    {
        string file_path = "Status/Unit/ally_unit_status";
        JsonHandler.ReadResult<AllyUnitSaveData> ally_unit_result = json_handler.ReadJsonFromResources<AllyUnitSaveData>(file_path);

        if (!ally_unit_result.is_success)
        {
            Debug.LogWarning($"{ally_unit_result.error_message}");
            return;
        }

        ally_unit_data = ally_unit_result.read_data.data;
    }
}

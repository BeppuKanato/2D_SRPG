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


    //マップ情報(2025/5/18時点 仮設定)
    public MapInfo map_info;

    /// <summary>
    /// ユニットデータをロード、生成する
    /// </summary>
    public void LoadAndGenerateUnits()
    {
        //全ユニットのデータをロード
        LoadFromJson();
        ally_units = ally_unit_data.Select((data, index) => InstantiateAllyUnit(data, index)).ToList();
    }

    /// <summary>
    /// 味方ユニットを生成する
    /// </summary>
    /// <param name="save_data">
    /// ユニットのデータ
    /// </param>
    /// <returns>
    /// 生成したユニット
    /// </returns>
    private AllyUnit InstantiateAllyUnit(AllyUnitSaveData save_data, int index)
    {
        Debug.Log(index);
        //初期位置のピクセル座標を取得
        Vector2 pixel_pos = GetPixelPosition(map_info.initial_pos_ally[index]);
        
        GameObject unit = ally_unit_factory.InstantiateFromStringKey(null, save_data.unit_id, pixel_pos, Quaternion.identity);
        //ユニットのプレハブがなかった場合
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
    /// タイルのピクセル座標を返す
    /// </summary>
    /// <param name="grid_pos">
    /// ピクセル座標を取得したいタイルのグリッド座標
    /// </param>
    /// <returns>
    /// ピクセル座標(Vector2)
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
    /// - 味方ユニットのステータス
    /// 
    /// を取得
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

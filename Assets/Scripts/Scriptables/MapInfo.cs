using UnityEngine;

[CreateAssetMenu(fileName = "MapInfo", menuName = "Scriptable Objects/MapInfo")]
public class MapInfo : ScriptableObject
{
    [field: SerializeField, Tooltip("マップデータのCSVファイル名")]
    public string csv_file_name { get; private set; }
    [field: SerializeField, Tooltip("マップの幅")]
    public int width { get; private set; }
    [field: SerializeField, Tooltip("マップの高さ")]
    public int height { get; private set; }
    [field: SerializeField, Tooltip("味方キャラクターの初期位置")]
    public Vector2Int[] initial_pos_ally { get; private set; }
    [field: SerializeField, Tooltip("敵キャラクターの初期位置")]
    public Vector2Int[] initial_pos_enemy { get; private set; }
}

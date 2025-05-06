using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [Tooltip("タイルの種類")]
    public TileType tile_type { get; private set; }
    [Tooltip("タイルの名前")]
    public string tile_name { get; private set; }
    [Tooltip("移動コスト")]
    public int move_cost { get; private set; }
    [Tooltip("回避補正")]
    public int avo_increase { get; private set; }
    [Tooltip("体力回復量")]
    public int hp_recover { get; private set; }

    /// <summary>
    /// ユニットのクラスがタイルを移動可能か返す
    /// </summary>
    /// <param name="unit_class">
    /// ユニットのクラス
    /// ユニット機能を作成するまで仮でint型
    /// </param>
    public abstract bool GetCanMove(int unit_class);
}

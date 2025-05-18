using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    [field: SerializeField, Tooltip("ユニットの名前")]
    public string unit_name { get; private set; }
    [field: SerializeField, Tooltip("グリッド座標")]
    public Vector2Int grid_pos { get; private set; }
    [field: SerializeField, Tooltip("ユニットのクラス")]
    public UnitClassSO unit_class { get; private set; }
    [field: SerializeField, Tooltip("ユニットの生存フラグ")]
    public bool is_alive { get; private set; }
    [field: SerializeField, Tooltip("ユニットの選択フラグ")]
    public bool is_selected { get; private set; }
    [field: SerializeField, Tooltip("ユニットの移動完了フラグ")]
    public bool has_moved { get; private set; }
    [field: SerializeField, Tooltip("アイコンの画像")]
    public Sprite icon { get; private set; }
    [field: SerializeField, Tooltip("装備中の武器")]
    public Weapon equip_weapon { get; private set; }
    [field: SerializeField, Tooltip("所持している武器")]
    public List<Weapon> have_weapons { get; private set; }

    [field: SerializeField, Tooltip("所持しているアイテム")]
    public List<Item> have_items { get; private set; }

    [field: SerializeField, Tooltip("選択可能な行動")]
    public List<IUnitAction> available_actions { get; private set; }

    [field: SerializeField, Tooltip("移動可能なタイル")]
    public List<Tile> moveable_tiles { get; private set; }

    [field: SerializeField, Tooltip("攻撃可能なタイル")]
    public List<Weapon> in_range_tiles { get; private set; }

    public virtual List<Vector2Int> GetMoveableArea(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log("移動可能なグリッド座標を返却します");
        return new List<Vector2Int>();
    }

    /// <summary>
    /// グリッド座標を直接代入
    /// </summary>
    /// <param name="grid_pos">
    /// グリッド座標
    /// </param>
    public void SetGridPos(Vector2Int grid_pos)
    {
        this.grid_pos = grid_pos;
    }

    public void GridMove(Vector2 target_pos, Vector2Int target_grid_pos)
    {
        Debug.Log($"座標: {target_pos} グリッド座標: {target_grid_pos}に移動");
        return;
    }

    public void SearchMoveableArea(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log($"移動可能なマスを探索します");
        return;
    }

    public void SearchInRangeTiles(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log($"攻撃可能なマスを探索します");
        return;
    }

    public void Select()
    {
        Debug.Log($"ユニット: {unit_name}が選択状態になりました");
        return;
    }

    public void DeSelect()
    {
        Debug.Log($"ユニット: {unit_name}の選択状態を解除します");
        return;
    }

    public void ResetTurnState()
    {
        Debug.Log($"ユニットの状態をターン開始のものに初期化します");
        return;
    }

    public void TakeDamege(int amount)
    {
        Debug.Log($"{amount}ダメージを受けます");
        return;
    }

    public void Heal(int amount)
    {
        Debug.Log($"{amount}HP回復します");
        return;
    }

    public void UpdateAvailableActions()
    {
        Debug.Log("選択可能な行動を更新します");
        return;
    }
}

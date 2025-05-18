using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    [field: SerializeField, Tooltip("���j�b�g�̖��O")]
    public string unit_name { get; private set; }
    [field: SerializeField, Tooltip("�O���b�h���W")]
    public Vector2Int grid_pos { get; private set; }
    [field: SerializeField, Tooltip("���j�b�g�̃N���X")]
    public UnitClassSO unit_class { get; private set; }
    [field: SerializeField, Tooltip("���j�b�g�̐����t���O")]
    public bool is_alive { get; private set; }
    [field: SerializeField, Tooltip("���j�b�g�̑I���t���O")]
    public bool is_selected { get; private set; }
    [field: SerializeField, Tooltip("���j�b�g�̈ړ������t���O")]
    public bool has_moved { get; private set; }
    [field: SerializeField, Tooltip("�A�C�R���̉摜")]
    public Sprite icon { get; private set; }
    [field: SerializeField, Tooltip("�������̕���")]
    public Weapon equip_weapon { get; private set; }
    [field: SerializeField, Tooltip("�������Ă��镐��")]
    public List<Weapon> have_weapons { get; private set; }

    [field: SerializeField, Tooltip("�������Ă���A�C�e��")]
    public List<Item> have_items { get; private set; }

    [field: SerializeField, Tooltip("�I���\�ȍs��")]
    public List<IUnitAction> available_actions { get; private set; }

    [field: SerializeField, Tooltip("�ړ��\�ȃ^�C��")]
    public List<Tile> moveable_tiles { get; private set; }

    [field: SerializeField, Tooltip("�U���\�ȃ^�C��")]
    public List<Weapon> in_range_tiles { get; private set; }

    public virtual List<Vector2Int> GetMoveableArea(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log("�ړ��\�ȃO���b�h���W��ԋp���܂�");
        return new List<Vector2Int>();
    }

    /// <summary>
    /// �O���b�h���W�𒼐ڑ��
    /// </summary>
    /// <param name="grid_pos">
    /// �O���b�h���W
    /// </param>
    public void SetGridPos(Vector2Int grid_pos)
    {
        this.grid_pos = grid_pos;
    }

    public void GridMove(Vector2 target_pos, Vector2Int target_grid_pos)
    {
        Debug.Log($"���W: {target_pos} �O���b�h���W: {target_grid_pos}�Ɉړ�");
        return;
    }

    public void SearchMoveableArea(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log($"�ړ��\�ȃ}�X��T�����܂�");
        return;
    }

    public void SearchInRangeTiles(Tile[,] tile_map, int map_width, int map_height)
    {
        Debug.Log($"�U���\�ȃ}�X��T�����܂�");
        return;
    }

    public void Select()
    {
        Debug.Log($"���j�b�g: {unit_name}���I����ԂɂȂ�܂���");
        return;
    }

    public void DeSelect()
    {
        Debug.Log($"���j�b�g: {unit_name}�̑I����Ԃ��������܂�");
        return;
    }

    public void ResetTurnState()
    {
        Debug.Log($"���j�b�g�̏�Ԃ��^�[���J�n�̂��̂ɏ��������܂�");
        return;
    }

    public void TakeDamege(int amount)
    {
        Debug.Log($"{amount}�_���[�W���󂯂܂�");
        return;
    }

    public void Heal(int amount)
    {
        Debug.Log($"{amount}HP�񕜂��܂�");
        return;
    }

    public void UpdateAvailableActions()
    {
        Debug.Log("�I���\�ȍs�����X�V���܂�");
        return;
    }
}

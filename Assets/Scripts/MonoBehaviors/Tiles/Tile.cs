using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [Tooltip("�^�C���̎��")]
    public TileType tile_type { get; private set; }
    [Tooltip("�^�C���̖��O")]
    public string tile_name { get; private set; }
    [Tooltip("�ړ��R�X�g")]
    public int move_cost { get; private set; }
    [Tooltip("���␳")]
    public int avo_increase { get; private set; }
    [Tooltip("�̗͉񕜗�")]
    public int hp_recover { get; private set; }

    /// <summary>
    /// ���j�b�g�̃N���X���^�C�����ړ��\���Ԃ�
    /// </summary>
    /// <param name="unit_class">
    /// ���j�b�g�̃N���X
    /// ���j�b�g�@�\���쐬����܂ŉ���int�^
    /// </param>
    public abstract bool GetCanMove(int unit_class);
}

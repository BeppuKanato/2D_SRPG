using UnityEngine;

[CreateAssetMenu(fileName = "MapInfo", menuName = "Scriptable Objects/MapInfo")]
public class MapInfo : ScriptableObject
{
    [field: SerializeField, Tooltip("�}�b�v�f�[�^��CSV�t�@�C����")]
    public string csv_file_name { get; private set; }
    [field: SerializeField, Tooltip("�}�b�v�̕�")]
    public int width { get; private set; }
    [field: SerializeField, Tooltip("�}�b�v�̍���")]
    public int height { get; private set; }
    [field: SerializeField, Tooltip("�����L�����N�^�[�̏����ʒu")]
    public Vector2Int[] initial_pos_ally { get; private set; }
    [field: SerializeField, Tooltip("�G�L�����N�^�[�̏����ʒu")]
    public Vector2Int[] initial_pos_enemy { get; private set; }
}

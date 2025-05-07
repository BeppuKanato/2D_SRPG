using UnityEngine;
using System.Collections.Generic;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    Transform field_parent;
    [SerializeField]
    private TileTypePrefabPair[] tile_pairs;
    private Dictionary<TileType, GameObject> tile_prefabs = new Dictionary<TileType, GameObject>();

    private void Awake()
    {
        //�\���̃f�[�^�����Ɏ����^�z����쐬
        foreach (TileTypePrefabPair tile_pair in tile_pairs)
        {
            tile_prefabs.Add(tile_pair.type, tile_pair.prefab);
        }
    }
    /// <summary>
    /// �^�C���𐶐����A�I�u�W�F�N�g��Tile��Ԃ�
    /// </summary>
    /// <param name="tile_type">�^�C���̎��</param>
    /// <param name="pos">����������W</param>
    /// <param name="rotation">��������ۂ̉�]</param>
    /// <returns>�I�u�W�F�N�g��Tile</returns>
    public Tile InstantiateTile(TileType tile_type, Vector2 pos, Quaternion rotation)
    {
        GameObject tile_obj = Instantiate(tile_prefabs[tile_type], pos, rotation);
        tile_obj.transform.SetParent(field_parent);
        return tile_obj.GetComponent<Tile>();
    }
    [System.Serializable]
    public struct TileTypePrefabPair
    {
        [field: SerializeField, Tooltip("�^�C���̎��")]
        public TileType type { get; private set; }
        [field: SerializeField, Tooltip("�^�C���̃v���n�u")]
        public GameObject prefab { get; private set; }
    }
}

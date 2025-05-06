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
        //構造体データを元に辞書型配列を作成
        foreach (TileTypePrefabPair tile_pair in tile_pairs)
        {
            tile_prefabs.Add(tile_pair.type, tile_pair.prefab);
        }
    }
    /// <summary>
    /// タイルを生成し、オブジェクトのTileを返す
    /// </summary>
    /// <param name="tile_type">タイルの種類</param>
    /// <param name="pos">生成する座標</param>
    /// <param name="rotation">生成する際の回転</param>
    /// <returns>オブジェクトのTile</returns>
    public Tile InstantiateTile(TileType tile_type, Vector2 pos, Quaternion rotation)
    {
        GameObject tile_obj = Instantiate(tile_prefabs[tile_type], pos, rotation);
        tile_obj.transform.SetParent(field_parent);
        return tile_obj.GetComponent<Tile>();
    }
    [System.Serializable]
    public struct TileTypePrefabPair
    {
        [field: SerializeField, Tooltip("タイルの種類")]
        public TileType type { get; private set; }
        [field: SerializeField, Tooltip("タイルのプレハブ")]
        public GameObject prefab { get; private set; }
    }
}

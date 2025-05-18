using UnityEngine;
using System.Collections.Generic;
public class FactoryWithIntKey : MonoBehaviour
{
    [SerializeField]
    IntKeyPrefabPair[] int_key_prefab_pairs;
    Dictionary<int, GameObject> prefabs;

    /// <summary>
    /// 指定キーのプレハブをインスタンス化
    /// インスタンス化したオブジェクトを返す
    /// </summary>
    /// <param name="parent">
    /// オブジェクトの親設定
    /// 親が不要: parent = null
    /// </param>
    /// <param name="key">
    /// int型のキー
    /// </param>
    /// <param name="pos">
    /// オブジェクトを生成する座標
    /// </param>
    /// <param name="rotation">
    /// オブジェクトの回転
    /// </param>
    /// <returns>
    /// インスタンス化したオブジェクト
    /// </returns>
    public GameObject InstantiateFromIntKey(Transform parent, int key, Vector2 pos, Quaternion rotation)
    {
        //辞書が初期化されてないとき
        if (prefabs == null)
        {
            ConvertArrayToDictionary();
        }

        if (!prefabs.ContainsKey(key))
        {
            Debug.LogWarning($"キー: {key}のプレハブが設定されていません");
            return null;
        }

        GameObject instance = Instantiate(prefabs[key], pos, rotation);
        if (parent != null)
        {
            instance.transform.SetParent(parent);
        }

        return instance;
    }
    /// <summary>
    /// 配列をkey: int, value: GameObjectの辞書に変換する
    /// </summary>
    void ConvertArrayToDictionary()
    {
        prefabs = new Dictionary<int, GameObject>();
        foreach (IntKeyPrefabPair pair in int_key_prefab_pairs)
        {
            if (!prefabs.ContainsKey(pair.key))
            {
                prefabs[pair.key] = pair.prefab;
            }
            else
            {
                Debug.LogWarning($"重複キー: {pair.key}");
            }
        }
    }
    [System.Serializable]
    public struct IntKeyPrefabPair 
    {
        public int key;
        public GameObject prefab;
    }

}

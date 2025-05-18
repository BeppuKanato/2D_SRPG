using System.Collections.Generic;
using UnityEngine;

public class FactoryWithStringKey : MonoBehaviour
{
    [SerializeField]
    StringKeyPrefabPair[] string_key_prefab_pairs;
    Dictionary<string, GameObject> prefabs;

    public GameObject InstantiateFromStringKey(Transform parent, string key, Vector2 pos, Quaternion rotation)
    {
        //��������̏ꍇ
        if (prefabs == null)
        {
            ConvertArrayToDictionary();
        }

        if (!prefabs.ContainsKey(key))
        {
            Debug.LogWarning($"�L�[: {key}�̃v���n�u���ݒ肳��Ă��܂���");
            return null;
        }

        GameObject instance = Instantiate(prefabs[key], pos, rotation);
        if (parent != null)
        {
            instance.transform.SetParent(parent);
        }
        
        return instance;
    }

    void ConvertArrayToDictionary()
    {
        prefabs = new Dictionary<string, GameObject>();
        foreach (StringKeyPrefabPair pair in string_key_prefab_pairs)
        {
            if (!prefabs.ContainsKey(pair.key))
            {
                prefabs[pair.key] = pair.prefab;
            }
            else
            {
                Debug.LogWarning($"�d���L�[: {pair.key}");
            }
        }
    }

    [System.Serializable]
    public struct StringKeyPrefabPair
    {
        public string key;
        public GameObject prefab;
    }
}
using UnityEngine;
using System.Collections.Generic;
public class FactoryWithIntKey : MonoBehaviour
{
    [SerializeField]
    IntKeyPrefabPair[] int_key_prefab_pairs;
    Dictionary<int, GameObject> prefabs;

    /// <summary>
    /// �w��L�[�̃v���n�u���C���X�^���X��
    /// �C���X�^���X�������I�u�W�F�N�g��Ԃ�
    /// </summary>
    /// <param name="parent">
    /// �I�u�W�F�N�g�̐e�ݒ�
    /// �e���s�v: parent = null
    /// </param>
    /// <param name="key">
    /// int�^�̃L�[
    /// </param>
    /// <param name="pos">
    /// �I�u�W�F�N�g�𐶐�������W
    /// </param>
    /// <param name="rotation">
    /// �I�u�W�F�N�g�̉�]
    /// </param>
    /// <returns>
    /// �C���X�^���X�������I�u�W�F�N�g
    /// </returns>
    public GameObject InstantiateFromIntKey(Transform parent, int key, Vector2 pos, Quaternion rotation)
    {
        //����������������ĂȂ��Ƃ�
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
    /// <summary>
    /// �z���key: int, value: GameObject�̎����ɕϊ�����
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
                Debug.LogWarning($"�d���L�[: {pair.key}");
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

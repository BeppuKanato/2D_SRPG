using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class JsonHandler
{
    /// <summary>
    /// Resources(�ǂݎ���p)����
    /// Json�t�@�C�������[�h���܂�
    /// </summary>
    /// <typeparam name="T">Json���R���o�[�g����^</typeparam>
    /// <param name="fine_name">
    /// Resources�t�H���_�ȉ���Json�t�@�C���p�X("Unit/ally_unit_status")
    /// </param>
    /// <returns>
    /// �ǂݎ�茋��(�����ہAJson�f�[�^�A�G���[���b�Z�[�W)
    /// </returns>
    public ReadResult<T> ReadJsonFromResources<T>(string fine_name)
    {
        ReadResult<T> result = new ReadResult<T>();
        TextAsset json_data = Resources.Load<TextAsset>(fine_name);
        if (json_data == null)
        {
            result.is_success = false;
            result.read_data = default;
            result.error_message = "json�t�@�C�������݂��Ă��܂���";

            return result;
        }

        string json = json_data.text;
        Debug.Log(json);
        JsonWrapper<T> json_data_array = JsonUtility.FromJson<JsonWrapper<T>>(json);
        result.is_success = true;
        result.read_data = json_data_array;
        result.error_message = null;

        return result;
    }
    /// <summary>
    /// PersistentDataPath(�ǂݏ����\)
    /// ����f�[�^Json�t�@�C�������[�h���܂�
    /// </summary>
    /// <typeparam name="T">Jsno���R���o�[�g����^</typeparam>
    /// <param name="file_name">
    /// Json�t�@�C���̃p�X�A�g���q�܂Ŋ܂߂�("Staus/Units/ally_unit_status.json")
    /// </param>
    /// <returns>
    ///�ǂݎ�茋��(�����ہAJson�f�[�^�A�G���[���b�Z�[�W)
    /// </returns>
    public ReadResult<T> ReadJsonFromPersistentDataPath<T>(string file_name)
    {
        ReadResult<T> result = new ReadResult<T>();
        string file_path = Path.Combine(Application.persistentDataPath, file_name);
        
        if (!File.Exists(file_path))
        {
            result.is_success = false;
            result.read_data = default;
            result.error_message = $"JSON�t�@�C��{file_path}�����݂��Ă��܂���";

            return result;
        }
        try
        {
            string json = File.ReadAllText(file_path);
            Debug.Log(json);
            JsonWrapper<T> jsonDataArray = JsonUtility.FromJson<JsonWrapper<T>>(json);
            result.is_success = true;
            result.read_data = jsonDataArray;
            result.error_message = null;
        }
        catch(System.Exception ex)
        {
            result.is_success = false;
            result.read_data = default;
            result.error_message = $"�G���[: {ex.Message}";
        }

        return result;
    }

    /// <summary>
    /// PersistentDataPath��Json�f�[�^����������
    /// </summary>
    /// <typeparam name="T">
    /// �������ރf�[�^�̌^
    /// </typeparam>
    /// <param name="file_name">
    /// �������ރt�@�C���̃p�X
    /// </param>
    /// <param name="write_data">
    /// �������ރf�[�^ ���K���z��
    /// </param>
    /// <returns>
    /// �������݌���(�����ہA�G���[���b�Z�[�W)
    /// </returns>
    public WriteResult WriteJson<T>(string file_name, T[] write_data)
    {
        WriteResult result = new WriteResult();

        JsonWrapper<T> wrapped_write_data = new JsonWrapper<T> { data = write_data };

        string write_string = JsonUtility.ToJson(wrapped_write_data);
        Debug.Log(write_string);
        string path = Path.Combine(Application.persistentDataPath, file_name);

        try
        {
            string directory = Path.GetDirectoryName(path);
            //�t�H���_���Ȃ��ꍇ
            if (!Directory.Exists(directory))
            {
                //�t�H���_���쐬
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, write_string);

            result.is_success = true;
            result.error_message = null;

            Debug.Log($"{path}�ɏ������݂܂���");
        }
        catch (System.Exception ex) 
        {
            result.is_success = false;
            result.error_message = $"�G���[: {ex.Message}";
        }

        return result;
    }

    /// <summary>
    /// Json�R���o�[�g�p�̌^��`
    /// </summary>
    /// <typeparam name="T">
    /// �R���o�[�g����^
    /// </typeparam>
    public class JsonWrapper<T>
    {
        public T[] data;
    }

    /// <summary>
    /// ���[�h�����̖߂�l�̌^
    /// </summary>
    /// <typeparam name="T">
    /// Json�f�[�^���R���o�[�g����^
    /// </typeparam>
    public struct ReadResult<T>
    {
        public bool is_success;
        public JsonWrapper<T> read_data;
        public string error_message;
    }
    /// <summary>
    /// �������ݎ��̖߂�l�̌^
    /// </summary>
    public struct WriteResult
    {
        public bool is_success;
        public string error_message;
    }
}

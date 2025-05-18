using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class JsonHandler
{
    /// <summary>
    /// Resources(読み取り専用)から
    /// Jsonファイルをロードします
    /// </summary>
    /// <typeparam name="T">Jsonをコンバートする型</typeparam>
    /// <param name="fine_name">
    /// Resourcesフォルダ以下のJsonファイルパス("Unit/ally_unit_status")
    /// </param>
    /// <returns>
    /// 読み取り結果(成功可否、Jsonデータ、エラーメッセージ)
    /// </returns>
    public ReadResult<T> ReadJsonFromResources<T>(string fine_name)
    {
        ReadResult<T> result = new ReadResult<T>();
        TextAsset json_data = Resources.Load<TextAsset>(fine_name);
        if (json_data == null)
        {
            result.is_success = false;
            result.read_data = default;
            result.error_message = "jsonファイルが存在していません";

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
    /// PersistentDataPath(読み書き可能)
    /// からデータJsonファイルをロードします
    /// </summary>
    /// <typeparam name="T">Jsnoをコンバートする型</typeparam>
    /// <param name="file_name">
    /// Jsonファイルのパス、拡張子まで含める("Staus/Units/ally_unit_status.json")
    /// </param>
    /// <returns>
    ///読み取り結果(成功可否、Jsonデータ、エラーメッセージ)
    /// </returns>
    public ReadResult<T> ReadJsonFromPersistentDataPath<T>(string file_name)
    {
        ReadResult<T> result = new ReadResult<T>();
        string file_path = Path.Combine(Application.persistentDataPath, file_name);
        
        if (!File.Exists(file_path))
        {
            result.is_success = false;
            result.read_data = default;
            result.error_message = $"JSONファイル{file_path}が存在していません";

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
            result.error_message = $"エラー: {ex.Message}";
        }

        return result;
    }

    /// <summary>
    /// PersistentDataPathにJsonデータを書き込む
    /// </summary>
    /// <typeparam name="T">
    /// 書き込むデータの型
    /// </typeparam>
    /// <param name="file_name">
    /// 書き込むファイルのパス
    /// </param>
    /// <param name="write_data">
    /// 書き込むデータ ※必ず配列
    /// </param>
    /// <returns>
    /// 書き込み結果(成功可否、エラーメッセージ)
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
            //フォルダがない場合
            if (!Directory.Exists(directory))
            {
                //フォルダを作成
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, write_string);

            result.is_success = true;
            result.error_message = null;

            Debug.Log($"{path}に書き込みました");
        }
        catch (System.Exception ex) 
        {
            result.is_success = false;
            result.error_message = $"エラー: {ex.Message}";
        }

        return result;
    }

    /// <summary>
    /// Jsonコンバート用の型定義
    /// </summary>
    /// <typeparam name="T">
    /// コンバートする型
    /// </typeparam>
    public class JsonWrapper<T>
    {
        public T[] data;
    }

    /// <summary>
    /// ロード処理の戻り値の型
    /// </summary>
    /// <typeparam name="T">
    /// Jsonデータをコンバートする型
    /// </typeparam>
    public struct ReadResult<T>
    {
        public bool is_success;
        public JsonWrapper<T> read_data;
        public string error_message;
    }
    /// <summary>
    /// 書き込み時の戻り値の型
    /// </summary>
    public struct WriteResult
    {
        public bool is_success;
        public string error_message;
    }
}

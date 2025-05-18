using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class CSVHandler
{
    /// <summary>
    /// CSVファイルをロードします
    /// </summary>
    /// <param name="csv_file_name">
    /// Resources フォルダ以下の CSV ファイルパス（例: "Map/map_01"）
    /// </param>
    /// <returns>
    /// 読み取り結果（成功可否、行データ、エラーメッセージ）
    /// </returns>
    public ReadResult ReadCSV(string csv_file_name)
    {
        ReadResult result = new ReadResult();
        TextAsset csv_text_asset = Resources.Load<TextAsset>(csv_file_name);
        //csvファイルの読み込み失敗
        if (csv_text_asset == null)
        {
            Debug.Log("CSVファイルの読み込みに失敗しました");

            result.is_success = false;
            result.read_data = new List<string[]>();
            result.error_message = "CSVファイルが存在していません";
            
            return result;
        }

        List<string[]> read_data = new List<string[]>();
        string[] lines = csv_text_asset.text.Split("\n");
        foreach (string line in lines)
        {
            //空文字、nullの場合処理しない
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            string[] values = line.Split(",");
            read_data.Add(values);
        }
        //CSVファイル内のデータがない場合
        if (read_data.Count == 0)
        {
            Debug.Log("CSVデータに中身がありません");

            result.is_success = false;
            result.read_data = read_data;
            result.error_message = "CSVデータに中身がありません";
            
            return result;
        }

        result.is_success = true;
        result.read_data = read_data;
        result.error_message = string.Empty;
        return result;
    }

    /// <summary>
    /// CSVファイルに書き込みます
    /// </summary>
    /// <param name="csv_file_name">
    /// 書き込みをするCSVファイル名
    /// </param>
    /// <param name="write_data">
    /// 書き込むデータ
    /// </param>
    /// <returns>
    /// 書き込み結果（成功可否、エラーメッセージ）
    /// </returns>
    public WriteResult WriteCSV(string csv_file_name, List<string[]> write_data)
    {
        WriteResult result = new WriteResult();
        string file_path = Path.Combine(Application.persistentDataPath, csv_file_name);

        try
        {
            using (StreamWriter writer = new StreamWriter(file_path, false, new System.Text.UTF8Encoding(false)))
            {
                foreach (string[] row in write_data)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }

            result.is_success = true;
            result.error_message = string.Empty;
            return result;
        }
        catch(System.Exception error)
        {
            result.is_success = false;
            result.error_message = error.Message;

            return result;
        }
    }
    /// <summary>
    /// ロード処理の戻り値の型
    /// </summary>
    public struct ReadResult
    {
        public bool is_success;
        public List<string[]> read_data;
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

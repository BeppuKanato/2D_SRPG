using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class CSVHandler
{
    /// <summary>
    /// CSV�t�@�C�������[�h���܂�
    /// </summary>
    /// <param name="csv_file_name">
    /// Resources �t�H���_�ȉ��� CSV �t�@�C���p�X�i��: "Map/map_01"�j
    /// </param>
    /// <returns>
    /// �ǂݎ�茋�ʁi�����ہA�s�f�[�^�A�G���[���b�Z�[�W�j
    /// </returns>
    public ReadResult ReadCSV(string csv_file_name)
    {
        ReadResult result = new ReadResult();
        TextAsset csv_text_asset = Resources.Load<TextAsset>(csv_file_name);
        //csv�t�@�C���̓ǂݍ��ݎ��s
        if (csv_text_asset == null)
        {
            Debug.Log("CSV�t�@�C���̓ǂݍ��݂Ɏ��s���܂���");

            result.is_success = false;
            result.read_data = new List<string[]>();
            result.error_message = "CSV�t�@�C�������݂��Ă��܂���";
            
            return result;
        }

        List<string[]> read_data = new List<string[]>();
        string[] lines = csv_text_asset.text.Split("\n");
        foreach (string line in lines)
        {
            //�󕶎��Anull�̏ꍇ�������Ȃ�
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            string[] values = line.Split(",");
            read_data.Add(values);
        }
        //CSV�t�@�C�����̃f�[�^���Ȃ��ꍇ
        if (read_data.Count == 0)
        {
            Debug.Log("CSV�f�[�^�ɒ��g������܂���");

            result.is_success = false;
            result.read_data = read_data;
            result.error_message = "CSV�f�[�^�ɒ��g������܂���";
            
            return result;
        }

        result.is_success = true;
        result.read_data = read_data;
        result.error_message = string.Empty;
        return result;
    }

    /// <summary>
    /// CSV�t�@�C���ɏ������݂܂�
    /// </summary>
    /// <param name="csv_file_name">
    /// �������݂�����CSV�t�@�C����
    /// </param>
    /// <param name="write_data">
    /// �������ރf�[�^
    /// </param>
    /// <returns>
    /// �������݌��ʁi�����ہA�G���[���b�Z�[�W�j
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
    /// ���[�h�����̖߂�l�̌^
    /// </summary>
    public struct ReadResult
    {
        public bool is_success;
        public List<string[]> read_data;
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

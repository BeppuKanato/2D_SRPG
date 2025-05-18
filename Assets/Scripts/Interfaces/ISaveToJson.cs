using UnityEngine;

/// <summary>
/// Jsonに保存するデータを保持する
/// マネージャークラスに実装
/// </summary>
public interface ISaveToJson
{
    /// <summary>
    /// Jsonにデータを保存
    /// </summary>
    void SaveToJson();
    /// <summary>
    /// Jsonからデータを取得
    /// </summary>
    void LoadFromJson();
}

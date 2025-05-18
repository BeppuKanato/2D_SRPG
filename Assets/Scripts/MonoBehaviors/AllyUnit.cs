using UnityEngine;

public class AllyUnit : Unit
{
    [field:SerializeField]
    public AllyUnitStatus unit_status { get; private set; }
    [field: SerializeField]
    public UnitGrowthRate growth_rate { get; private set; }

    /// <summary>
    /// ステータスを設定する
    /// </summary>
    /// <param name="save_data">
    /// ステータスデータ(セーブデータ形式)
    /// </param>
    public void SetStatus(AllyUnitSaveData save_data)
    {
        AllyUnitStatus status = new AllyUnitStatus(save_data);
        this.unit_status= status;
    }
    public void GainExp(int exp_amount)
    {

    }

    public void LevelUp()
    {

    }

    public void ClassPromote()
    {

    }
}
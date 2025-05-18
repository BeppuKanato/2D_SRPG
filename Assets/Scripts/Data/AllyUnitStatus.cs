using UnityEngine;

[System.Serializable]
public class AllyUnitStatus: UnitStatus
{
    [field: SerializeField]
    public int exp { get; private set; }

    public AllyUnitStatus(AllyUnitSaveData save_data):base(
            save_data.max_hp,
            save_data.current_hp,
            save_data.str,
            save_data.m_power,
            save_data.tec,
            save_data.agi,
            save_data.def,
            save_data.m_def,
            save_data.luck,
            save_data.physique,
            save_data.level
        )
    {
        this.exp = save_data.exp;
    }
}

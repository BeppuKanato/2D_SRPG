using UnityEngine;

[System.Serializable]
public abstract class UnitStatus
{
    [field: SerializeField]
    public int max_hp { get; private set; }
    [field: SerializeField]
    public int current_hp { get; private set; }
    [field: SerializeField]
    public int str { get; private set; }
    [field: SerializeField]
    public int m_power { get; private set; }
    [field: SerializeField]
    public int tec { get; private set; }
    [field: SerializeField]
    public int agi { get; private set; }
    [field: SerializeField]
    public int def { get; private set; }
    [field: SerializeField]
    public int m_def { get; private set; }
    [field: SerializeField]
    public int luck { get; private set; }
    [field: SerializeField]
    public int physique { get; private set; }
    [field: SerializeField]
    public int level { get; private set; }


    public UnitStatus(int max_hp, int current_hp,int str, int m_power, int tec, int agi, int def, int m_def, int luck, int physique, int level)
    {
        this.max_hp = max_hp;
        this.current_hp = current_hp;
        this.str = str;
        this.m_power = m_power;
        this.tec = tec;
        this.agi = agi;
        this.def = def;
        this.m_def = m_def;
        this.luck = luck;
        this.physique = physique;
        this.level = level;
    }
}

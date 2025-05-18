using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public struct AllyUnitSaveData
{
    public string unit_id;
    public int current_hp;
    public int max_hp;
    public int str;
    public int m_power;
    public int tec;
    public int agi;
    public int def;
    public int m_def;
    public int luck;
    public int physique;
    public int level;
    public int exp;
    public string equip_weapon_id;
    public List<string> have_weapon_ids;
    public List<string> have_item_ids;
}
